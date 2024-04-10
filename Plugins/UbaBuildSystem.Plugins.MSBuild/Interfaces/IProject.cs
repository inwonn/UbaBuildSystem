using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UbaBuildSystem.Plugins.MSBuild.Interfaces
{
    internal interface IProject
    {
        IEnumerable<IClCompile> ClCompiles { get; }
    }
}
