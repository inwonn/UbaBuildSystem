
using System.Reflection;
using PathStatics = UbaBuildSystem.Plugins.Core.Statics.Path;
using UbaBuildSystem.Plugins.Core.Interfaces;

namespace UbaBuildSystem.Plugins
{
    internal class PluginLoadContextManager
    {
        private Dictionary<string, PluginLoadContext> _plugins { get; set; }

        public PluginLoadContextManager()
        {
            _plugins = new Dictionary<string, PluginLoadContext>();
            LoadPlugins();
        }

        internal ICommand? GetCommand(string pluginName, string commandName)
        {
            if (_plugins.TryGetValue(pluginName, out PluginLoadContext? plugin) == false)
            {
                return null;
            }
            
            //foreach (Type type in plugin.GetTypes())
            //{
            //    if (type.GetInterface(nameof(ICommand)) == null)
            //    {
            //        continue;
            //    }

            //    ICommand? command = Activator.CreateInstance(type) as ICommand;
            //    if (command == null)
            //    {
            //        continue;
            //    }

            //    if (command.Name == commandName)
            //    {
            //        return command;
            //    }
            //}

            return null;
        }

        private void LoadPlugins()
        {
            string pluginsRoot = PathStatics.GetPluginsRoot();
            if (!Directory.Exists(pluginsRoot))
            {
                Console.WriteLine("No plugins found.");
                return;
            }

            string[] pluginDirectories = Directory.GetDirectories(pluginsRoot);
            foreach (string pluginDirectory in pluginDirectories)
            {
                string? directoryName = Path.GetFileName(pluginDirectory);
                if (string.IsNullOrEmpty(directoryName))
                {
                    Console.WriteLine("Failed to get directory name.");
                    continue;
                }

                string assemblyFullPath = Path.Combine(pluginDirectory, $"{directoryName}.dll");
                if (!File.Exists(assemblyFullPath))
                {
                    Console.WriteLine($"No manifest found for plugin {directoryName}.");
                    continue;
                }

                PluginLoadContext? pluginLoadContext = Load(assemblyFullPath);
                if (pluginLoadContext == null)
                {
                    Console.WriteLine($"Failed to load plugin {assemblyFullPath}.");
                    continue;
                }

                _plugins.Add(directoryName, pluginLoadContext);
            }
        }

        private PluginLoadContext? Load(string assemblyFullPath)
        {
            string root = PathStatics.GetPluginsRoot();
            if (!Directory.Exists(root))
            {
                Console.WriteLine("No plugins found.");
                return null;
            }

            PluginLoadContext loadContext = new PluginLoadContext(assemblyFullPath);
            return loadContext.LoadFromAssemblyName(new AssemblyName(Path.GetFileNameWithoutExtension(assemblyFullPath)));
        }
    }
}
