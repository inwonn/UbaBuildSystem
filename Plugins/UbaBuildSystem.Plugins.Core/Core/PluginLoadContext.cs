using System.Reflection;
using System.Runtime.Loader;

namespace UbaBuildSystem.Plugins.Core.Core
{
    public class PluginLoadContext : AssemblyLoadContext
    {
        private string _assemblyDir;
        public string[]? RuntimeDependencylDir { get; set; }

        public PluginLoadContext(string assemblyDir)
        {
            _assemblyDir = assemblyDir;

            Resolving += RuntimeAssemblyLoadContext_Resolving;
        }

        private Assembly? RuntimeAssemblyLoadContext_Resolving(AssemblyLoadContext arg1, AssemblyName arg2)
        {
            string? aseemblyName = arg2.Name;
            if (string.IsNullOrEmpty(aseemblyName))
            {
                return null;
            }

            string assemblyPath = System.IO.Path.Combine(_assemblyDir, aseemblyName + ".dll");
            if (System.IO.File.Exists(assemblyPath))
            {
                return LoadFromAssemblyPath(assemblyPath);
            }

            assemblyPath = System.IO.Path.Combine(_assemblyDir, "runtimes", aseemblyName + ".dll");
            if (System.IO.File.Exists(assemblyPath))
            {
                return LoadFromAssemblyPath(assemblyPath);
            }

            if (RuntimeDependencylDir != null)
            {
                foreach (string additionalDir in RuntimeDependencylDir)
                {
                    assemblyPath = System.IO.Path.Combine(additionalDir, aseemblyName + ".dll");
                    if (System.IO.File.Exists(assemblyPath))
                    {
                        return LoadFromAssemblyPath(assemblyPath);
                    }
                }
            }

            Console.WriteLine($"Failed to resolve assembly: {aseemblyName}");
            return null;
        }
    }
}
