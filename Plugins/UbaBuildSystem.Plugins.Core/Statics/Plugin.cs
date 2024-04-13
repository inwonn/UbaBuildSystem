using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UbaBuildSystem.Plugins.Core.Core;

namespace UbaBuildSystem.Plugins.Core.Statics
{
    public class Plugin
    {
        public static void LoadPlugins()
        {
            PluginManager.GetGlobalPluginManager().LoadPlugins();
        }

        public static void UnloadPlugins()
        {
            PluginManager.GetGlobalPluginManager().UnloadPlugins();
        }

        public static PluginBase? GetPlugin(string pluginName)
        {
            return PluginManager.GetGlobalPluginManager().GetPlugin(pluginName);
        }

        public static Assembly? LoadAssembly(string pluginName, string assemblyname)
        {
            PluginBase? plugin = GetPlugin(pluginName);
            if (plugin == null)
            {
                return null;
            }

            return plugin.LoadAssembly(assemblyname);
        }
    }
}
