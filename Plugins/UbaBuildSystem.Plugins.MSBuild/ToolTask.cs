using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UbaBuildSystem.Plugins.MSBuild
{
    public class ToolTask
    {
        internal enum ToolTaskStatus
        {
            Created = 0,
            Cancelled = 1,
            RanToCompletion = 2,
            Faulted = 3,
            Running = 4,
        };

        public string? CommandLine { get; set; }
        public string? CurrentDirectory { get; set; }
        public List<string>? Envs { get; set; }
        internal ToolTaskStatus Status { get; set; }
    }
}
