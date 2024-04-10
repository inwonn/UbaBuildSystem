using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UbaBuildSystem.Plugins.Core.Statics
{
    public static class Path
    {
        public static string GetPluginsRoot()
        {
            return System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location)!, "Plugins");
        }
    }
}
