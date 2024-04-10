using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UbaBuildSystem.Plugins.MSBuild.Interfaces
{
    internal interface ISolution
    {
        IEnumerable<IProject> Projects { get; }

        void Parse(string solutionFile);
    }
}
