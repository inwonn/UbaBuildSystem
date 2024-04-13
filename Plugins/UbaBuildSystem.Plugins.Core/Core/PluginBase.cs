using Microsoft.Extensions.Configuration;
using System.Reflection;
using UbaBuildSystem.Plugins.Core.Interfaces;
using UbaBuildSystem.Plugins.Core.Statics;
using Path = System.IO.Path;
using PathStatics = UbaBuildSystem.Plugins.Core.Statics.Path;

namespace UbaBuildSystem.Plugins.Core.Core
{
    public abstract class PluginBase
    {
        private string _pluginDir;
        private Assembly _pluginAssembly;
        private PluginLoadContext? _pluginLoadContext;
        private PluginManafest? _pluginMenifest;

        public PluginBase(Assembly pluginAssembly, string pluginDir)
        {
            _pluginAssembly = pluginAssembly;
            _pluginDir = pluginDir;
        }

        protected string PluginDir => _pluginDir;
        protected PluginLoadContext? PluginLoadContext => _pluginLoadContext;
        public virtual void PreLoad() { }
        public virtual void PostLoad() { }
        public virtual void PreUnload() { }
        public virtual void PostUnload() { }

        public void Load(PluginLoadContext pluginLoadContext)
        {
            _pluginLoadContext = pluginLoadContext;

            PreLoad();

            string pluginConfig = Path.Combine(_pluginDir, "plugin.json");
            if (!File.Exists(pluginConfig))
            {
                throw new FileNotFoundException("Plugin configuration file not found.");
            }

            _pluginMenifest = new PluginManafest();
            
            IConfigurationRoot configuration = new ConfigurationBuilder().AddJsonFile(pluginConfig).Build();
            configuration.Bind(_pluginMenifest);

            PostLoad();
        }

        public void Unload()
        {
            PreUnload();
            PostUnload();
        }

        public ICommand? GetCommand(string commandName)
        {
            foreach (Type type in _pluginAssembly.GetTypes())
            {
                if (type.GetInterface(nameof(ICommand)) == null)
                {
                    continue;
                }

                ICommand? command = Activator.CreateInstance(type) as ICommand;
                if (command == null)
                {
                    continue;
                }

                if (type.Name == commandName)
                {
                    return command;
                }
            }

            return null;
        }

        public Assembly? LoadAssembly(string assemblyName)
        {
            return _pluginLoadContext?.LoadFromAssemblyName(new AssemblyName(assemblyName));
        }
    }
}
