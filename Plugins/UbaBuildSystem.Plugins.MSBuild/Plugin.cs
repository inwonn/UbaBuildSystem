using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UbaBuildSystem.Plugins.Core.Core;

namespace UbaBuildSystem.Plugins.MSBuild
{
    internal class Plugin : PluginBase
    {
        public Plugin(Assembly pluginAssembly, string pluginDir) : base(pluginAssembly, pluginDir)
        {
        }
    }
}
