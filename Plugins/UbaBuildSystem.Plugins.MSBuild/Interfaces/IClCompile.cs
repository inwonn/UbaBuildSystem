using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UbaBuildSystem.Plugins.MSBuild.Interfaces
{
    internal interface IClCompile
    {
        IEnumerable<string> SourceFiles { get; }
        bool CreatePch { get; }
        bool UsePch { get; }
        string PdbOutput { get; }
        string PchOutput { get; }
    }
}
