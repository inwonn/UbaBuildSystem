using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UbaBuildSystem.Plugins.Core
{
    internal interface IManifest
    {
        Guid Id { get; }
        Version Version { get; }
        string Description { get; }
        string Authors { get; }
        string ProjectUrl { get; }
        string ReadMe { get; }
        string ReleaseNotes { get; }
        string RepositoryUrl { get; }
        string[] AdditionalDependencies  { get; }
    }
}
