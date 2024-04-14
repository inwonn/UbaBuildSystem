using System.Reflection;
using System.Runtime.Loader;

namespace UbaBuildSystem.Plugins.Core.Core
{
    public class PluginLoadContext : AssemblyLoadContext
    {
        private string _assemblyDir;
        public string[]? RuntimeDependencylDir { get; set; }
        public string[]? RuntimeUnmanagedDependencylDir { get; set; }

        public PluginLoadContext(string assemblyDir)
        {
            _assemblyDir = assemblyDir;

            Resolving += RuntimeAssemblyLoadContext_Resolving;
            ResolvingUnmanagedDll += PluginLoadContext_ResolvingUnmanagedDll;
        }

        private IntPtr PluginLoadContext_ResolvingUnmanagedDll(Assembly arg1, string arg2)
        {
            string? aseemblyName = arg2;
            if (string.IsNullOrEmpty(aseemblyName))
            {
                return IntPtr.Zero;
            }

            string assemblyPath = System.IO.Path.Combine(_assemblyDir, aseemblyName + ".dll");
            if (System.IO.File.Exists(assemblyPath))
            {
                return LoadUnmanagedDllFromPath(assemblyPath);
            }

            assemblyPath = System.IO.Path.Combine(_assemblyDir, "runtimes", aseemblyName + ".dll");
            if (System.IO.File.Exists(assemblyPath))
            {
                return LoadUnmanagedDllFromPath(assemblyPath);
            }

            if (RuntimeUnmanagedDependencylDir != null)
            {
                foreach (string additionalDir in RuntimeUnmanagedDependencylDir)
                {
                    assemblyPath = System.IO.Path.Combine(additionalDir, aseemblyName + ".dll");
                    if (System.IO.File.Exists(assemblyPath))
                    {
                        return LoadUnmanagedDllFromPath(assemblyPath);
                    }
                }
            }

            Console.WriteLine($"Failed to resolve assembly: {aseemblyName}");
            return IntPtr.Zero;
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
