using System.IO;
using System.Reflection;
using UbaBuildSystem.Plugins.Core.Interfaces;
using PathStatics = UbaBuildSystem.Plugins.Core.Statics.Path;

namespace UbaBuildSystem.Plugins.Core.Core
{
    internal class PluginManager
    {
        private static PluginManager? GlobalPluginManaer;
        internal static PluginManager GetGlobalPluginManager()
        {
            GlobalPluginManaer ??= new PluginManager();
            return GlobalPluginManaer;
        }

        private Dictionary<string, PluginBase> _plugins { get; set; } = new Dictionary<string, PluginBase>();

        internal PluginBase? GetPlugin(string pluginName)
        {
            if (_plugins.ContainsKey(pluginName))
            {
                return _plugins[pluginName];
            }

            return null;
        }

        internal void LoadPlugins()
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
                string? pluginName = Path.GetFileName(pluginDirectory);
                if (string.IsNullOrEmpty(pluginName))
                {
                    Console.WriteLine("Failed to get directory name.");
                    continue;
                }

                string assemblyFullPath = Path.Combine(pluginDirectory, $"{pluginName}.dll");
                if (!File.Exists(assemblyFullPath))
                {
                    Console.WriteLine($"No manifest found for plugin {pluginName}.");
                    continue;
                }

                PluginLoadContext pluginLoadContext = new PluginLoadContext(pluginDirectory);
                Assembly pluginAssembly = pluginLoadContext.LoadFromAssemblyPath(assemblyFullPath);
                foreach (var pluginType in pluginAssembly.GetTypes())
                {
                    if (!pluginType.IsSubclassOf(typeof(PluginBase)))
                    {
                        continue;
                    }

                    PluginBase? plugin = Activator.CreateInstance(pluginType, new object[] { pluginAssembly, pluginDirectory }) as PluginBase;
                    if (plugin != null)
                    {
                        plugin.Load(pluginLoadContext);
                        _plugins.Add(pluginName, plugin);
                    }
                }
            }
        }

        internal void UnloadPlugins()
        {
            foreach (PluginBase plugin in _plugins.Values)
            {
                plugin.Unload();
            }
        }
    }
}
