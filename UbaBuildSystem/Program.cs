using System.Reflection;
using UbaBuildSystem.Plugins;
using UbaBuildSystem.Plugins.Core.Interfaces;
using PluginsPath = UbaBuildSystem.Plugins.Core.Statics.Path;
using SystemIOPath = System.IO.Path;

namespace UbaBuildSystem
{
    class Program
    {
        private static Assembly CurrentDomain_AssemblyResolve1(object sender, ResolveEventArgs args)
        {
            // 추가 DLL이 존재하는 경로를 여기에 지정하세요.
            string additionalDllPath = @"C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Microsoft\VC\v170\";

            // 요청된 어셈블리 이름 가져오기
            string assemblyName = new AssemblyName(args.Name).Name;

            // 요청된 어셈블리가 추가 DLL 경로에 존재하는지 확인
            string assemblyPath = SystemIOPath.Combine(additionalDllPath, assemblyName + ".dll");
            if (File.Exists(assemblyPath))
            {
                return Assembly.LoadFrom(assemblyPath);
            }

            return null; // 요청된 어셈블리를 찾을 수 없음
        }
        private static Assembly CurrentDomain_AssemblyResolve2(object sender, ResolveEventArgs args)
        {
            // 추가 DLL이 존재하는 경로를 여기에 지정하세요.
            string additionalDllPath = @"C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\";

            // 요청된 어셈블리 이름 가져오기
            string assemblyName = new AssemblyName(args.Name).Name;

            // 요청된 어셈블리가 추가 DLL 경로에 존재하는지 확인
            string assemblyPath = SystemIOPath.Combine(additionalDllPath, assemblyName + ".dll");
            if (File.Exists(assemblyPath))
            {
                return Assembly.LoadFrom(assemblyPath);
            }

            return null; // 요청된 어셈블리를 찾을 수 없음
        }

        static void Main(string[] args)
        {
            try
            {
                //AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve1;
                //AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve2;
                //Assembly CPPTasksAssembly = AppDomain.CurrentDomain.Load("Microsoft.Build.CPPTasks.Common");

                //var CLType = CPPTasksAssembly.GetType("Microsoft.Build.CPPTasks.CL");
                //var CLTask = Activator.CreateInstance(CLType);

                //var CLCommandLineType = CPPTasksAssembly.GetType("Microsoft.Build.CPPTasks.CLCommandLine");
                //var CLCommandLineTask = Activator.CreateInstance(CLCommandLineType);

                //if (args.Length == 1 && args[0] == "/d")
                //{
                //    Console.WriteLine("Waiting for any key...");
                //    Console.ReadLine();
                //}

                PluginLoadContextManager pluginLoadContextManager = new PluginLoadContextManager();
                ICommand? command = pluginLoadContextManager.GetCommand("UbaBuildSystem.Plugins.MSBuild", "RebuildCommand");

                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}