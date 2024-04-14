using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using PathStatics = UbaBuildSystem.Plugins.Core.Statics.Path;

namespace UbaBuildSystem.Plugins.MSBuild
{

    internal class BuildService : IDisposable
    {
        #region PInvoke_kernel32.dll
        const uint LOAD_LIBRARY_SEARCH_DEFAULT_DIRS = 0x00001000;

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool SetDefaultDllDirectories(uint DirectoryFlags);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        static extern int AddDllDirectory(string NewDirectory);
        #endregion

        #region PInvoke_UbaMSBuild.Core.dll
        [DllImport("UbaMSBuild.Core.dll", CharSet = CharSet.Unicode)]
        public extern static IntPtr CreateProcessWithDll(string commandLine, string buildId, string dllPath, out uint pid);

        [DllImport("UbaMSBuild.Core.dll", CharSet = CharSet.Unicode)]
        public extern static uint GetToolTaskCount(string buildId);

        [DllImport("UbaMSBuild.Core.dll", CharSet = CharSet.Unicode)]
        public extern static bool GetToolTask(string buildId, int toolTaskId, out IntPtr outToolTask, out uint size);

        [DllImport("UbaMSBuild.Core.dll", CharSet = CharSet.Unicode)]
        public extern static bool SetToolTaskStatus(string buildId, int toolTaskId, uint toolTaskStatus);

        [DllImport("UbaMSBuild.Core.dll", CharSet = CharSet.Unicode)]
        public extern static uint GetToolTaskStatus(string buildId, int toolTaskId);
        #endregion

        private Dictionary<int/*toolTaskId*/, ToolTask> _toolTasks = new Dictionary<int, ToolTask>();

        public BuildService()
        {

        }

        public void Dispose()
        {
        }

        public Task<int> BuildAsync()
        {
            SetDefaultDllDirectories(LOAD_LIBRARY_SEARCH_DEFAULT_DIRS);
            AddDllDirectory(Path.Combine(PathStatics.GetPluginsRoot(), "UbaBuildSystem.Plugins.MSBuild", "runtimes"));

            string sln = @"D:\Git\UbaMSBuild\Binaries\x64\Debug\UbaMSBuild.Core.Test\TestData\BuildTest\BuildTest.sln";
            string dllPath = @"D:\Git\UbaBuildSystem\Plugins\UbaBuildSystem.Plugins.MSBuild\runtimes\UbaMSBuild.Core.dll";
            string commandLine = $"\"C:\\Program Files\\Microsoft Visual Studio\\2022\\Community\\Common7\\IDE\\devenv.exe\" {sln} /Rebuild \"Debug|x64\"";
            string buildId = Guid.NewGuid().ToString();
            Environment.SetEnvironmentVariable("MSBUILDDISABLENODEREUSE", "1");
            CreateProcessWithDll(commandLine, buildId, dllPath, out uint pid);
            Process devenv = Process.GetProcessById((int)pid);

            while (devenv.HasExited == false)
            {
                uint toolTaskCount = GetToolTaskCount(buildId);
                for (int toolTaskId = 0; toolTaskId < toolTaskCount; toolTaskId++)
                {
                    if (GetToolTaskStatus(buildId, toolTaskId) == 4)
                    {
                        if (GetToolTask(buildId, toolTaskId, out IntPtr toolTaskPtr, out uint size))
                        {
                            byte[] toolTaskBytes = new byte[size];
                            Marshal.Copy(toolTaskPtr, toolTaskBytes, 0, toolTaskBytes.Length);
                            string jsonText = Encoding.UTF8.GetString(toolTaskBytes);
                            try
                            {
                                ToolTask? toolTask = JsonSerializer.Deserialize<ToolTask>(jsonText);
                                if (toolTask != null)
                                {
                                    _toolTasks.Add(toolTaskId, toolTask);
                                }
                            }
                            catch
                            {
                                SetToolTaskStatus(buildId, toolTaskId, (uint)ToolTask.ToolTaskStatus.Faulted);
                            }
                        }
                    }
                }

                foreach (var toolTaskPair in _toolTasks)
                {
                    int toolTaskId = toolTaskPair.Key;
                    var toolTaskState = toolTaskPair.Value;
                    switch (toolTaskState.Status)
                    {
                        case ToolTask.ToolTaskStatus.RanToCompletion:
                        default:
                            SetToolTaskStatus(buildId, toolTaskId, (uint)ToolTask.ToolTaskStatus.RanToCompletion);
                            _toolTasks.Remove(toolTaskId);
                            break;
                    }
                }
            }

            return Task.FromResult(0);
            //return Task.FromResult(devenv.ExitCode);
        }

        private void LoadVsSolution()
        {
            VsSolution sln = new VsSolution(@"D:\Git\UbaMSBuild\Binaries\x64\Debug\UbaMSBuild.Core.Test\TestData\BuildTest\BuildTest.sln");
            sln.Load();
        }

        private void LoadVcxproj()
        {
            
        }
    }
}
