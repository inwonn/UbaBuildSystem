using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UbaBuildSystem.Plugins.Core
{
    internal interface IPluginContext
    {
        void PostLoad();
        void PreUnLoad();

    }
}
