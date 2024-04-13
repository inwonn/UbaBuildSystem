using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UbaBuildSystem.Plugins.Core.Core
{
    public class PluginManafest
    {
        public string? Id { get; set; }
        public string? Version { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Authors { get; set; }
        public string? ReadMe { get; set; }
        public string? ReleaseNotes { get; set; }
        public string? RepositoryUrl { get; set; }
    }
}
