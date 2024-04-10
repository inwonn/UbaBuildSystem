using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UbaBuildSystem.Plugins.MSBuild.Interfaces;

namespace UbaBuildSystem.Plugins.MSBuild.Build
{
    internal class Solution : ISolution
    {
        public IEnumerable<IProject> Projects => throw new NotImplementedException();

        public void Parse(string solutionFile)
        {
            throw new NotImplementedException();
        }
    }
}
