using EpicGames.Core;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UbaBuildSystem.Plugins.MSBuild.Generated;

namespace UbaBuildSystem.Plugins.MSBuild
{

    internal class BuildService : IDisposable
    {
        #region PInvoke_UbaMSBuild.Core.dll
        [DllImport("UbaMSBuild.Core.dll", CharSet = CharSet.Unicode)]
        public extern static IntPtr CreateProcessWithDll(string commandLine, string buildId, string dllPath);

        [DllImport("UbaMSBuild.Core.dll", CharSet = CharSet.Unicode)]
        public extern static uint HostGetToolTaskCount(string buildId);

        [DllImport("UbaMSBuild.Core.dll", CharSet = CharSet.Unicode)]
        public extern static bool HostGetToolTask(string buildId, int toolTaskId, out IntPtr outToolTask, out uint size);

        [DllImport("UbaMSBuild.Core.dll", CharSet = CharSet.Unicode)]
        public extern static bool HostSetToolTaskStatus(string buildId, int toolTaskId, uint toolTaskStatus);
        #endregion

        private Dictionary<int/*toolTaskId*/, ToolTaskState> _toolTasks = new Dictionary<int, ToolTaskState>();
        private UbaSessionServer _ubaSessionServer = new UbaSessionServer();

        public BuildService()
        {
            ILogger Logger = Log.Logger;
            _ubaSessionServer.Start(Logger);
        }

        public void Dispose()
        {
            _ubaSessionServer?.Dispose();
        }

        public async Task<int> BuildAsync()
        {
            //string commandLine = "";
            string buildId = Guid.NewGuid().ToString();
            //string dllPath = "";
            //IntPtr hProcess = CreateProcessWithDll(commandLine, buildId, dllPath);
            IntPtr hProcess = IntPtr.Zero;
            Process devenv = Process.GetProcesses().First(p => p.Handle == hProcess);


            await ProcessToolTaskAsync(buildId, devenv);

            return 0;
        }

        private Task<int> ProcessToolTaskAsync(string buildId, Process devenv)
        {
            return Task.Run(() =>
            {
                while (devenv.HasExited == false)
                {
                    uint toolTaskCount = HostGetToolTaskCount(buildId);
                    for (int toolTaskId = 0; toolTaskId < toolTaskCount; toolTaskId++)
                    {
                        if (HostGetToolTask(buildId, toolTaskId, out IntPtr toolTaskPtr, out uint size))
                        {
                            byte[] toolTaskBytes = new byte[size];
                            Marshal.Copy(toolTaskPtr, toolTaskBytes, 0, toolTaskBytes.Length);
                            ToolTask toolTask = ToolTask.Parser.ParseFrom(toolTaskBytes);

                            _toolTasks[toolTaskId] = new ToolTaskState(toolTask, ToolTaskStatus.Created);
                        }
                    }

                    foreach (var toolTaskPair in _toolTasks)
                    {
                        int toolTaskId = toolTaskPair.Key;
                        var toolTaskState = toolTaskPair.Value;
                        if (toolTaskState.Status == ToolTaskStatus.Created)
                        {
                        }
                        else if (toolTaskState.IsRunning())
                        {
                        }
                        else if (toolTaskState.IsCompleted())
                        {
                            HostSetToolTaskStatus(buildId, toolTaskId, (uint)ToolTaskStatus.Completed);
                            _toolTasks.Remove(toolTaskId);
                        }
                        else if (toolTaskState.IsFaulted())
                        {
                            HostSetToolTaskStatus(buildId, toolTaskId, (uint)ToolTaskStatus.Faulted);
                            _toolTasks.Remove(toolTaskId);
                        }
                    }
                }

                return devenv.ExitCode;
            });
        }
    }
}
