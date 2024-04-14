using System.Diagnostics.Metrics;
using System.Reflection;
using UbaBuildSystem.Plugins;
using UbaBuildSystem.Plugins.Core.Core;
using UbaBuildSystem.Plugins.Core.Interfaces;
using UbaBuildSystem.Plugins.Core.Statics;
using PluginsPath = UbaBuildSystem.Plugins.Core.Statics.Path;
using SystemIOPath = System.IO.Path;

namespace UbaBuildSystem
{
    class Program
    {
        static int Main(string[] args)
        {
            Plugin.LoadPlugins();
            PluginBase? plugin = Plugin.GetPlugin("UbaBuildSystem.Plugins.MSBuild");
                
            ICommand? command = plugin?.GetCommand("BuildCommand");
            bool? isSuccess = command?.ExecuteAsync().Result;

            return 0;
        }
    }
}