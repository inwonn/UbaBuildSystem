using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UbaBuildSystem.Plugins.MSBuild.Generated;

namespace UbaBuildSystem.Plugins.MSBuild
{

    internal class BuildService
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


        class ToolTaskState
        {
            public ToolTask Task { get; set; }
            public ToolTaskStatus Status { get; set; }
            public object[]? LinkedActions { get; set; }

            public ToolTaskState(ToolTask task, ToolTaskStatus status)
            {
                Task = task;
                Status = status;
                LinkedActions = task.ToLinkedActions();
            }
        }

        private Dictionary<int/*toolTaskId*/, ToolTaskState> _toolTasks = new Dictionary<int, ToolTaskState>();

        public BuildService()
        {
            //Console.WriteLine(HostGetToolTaskCount("Test"));
        }

        public Task<int> Build()
        {
            //string commandLine = "";
            string buildId = Guid.NewGuid().ToString();
            //string dllPath = "";
            //IntPtr hProcess = CreateProcessWithDll(commandLine, buildId, dllPath);
            IntPtr hProcess = IntPtr.Zero;
            Process devenv = Process.GetProcesses().First(p => p.Handle == hProcess);

            return ProcessToolTaskAsync(buildId, devenv);
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
                        switch (toolTaskState.Status)
                        {
                            case ToolTaskStatus.Created:
                                {
                                    DelegateToUba(toolTaskState.Task);
                                    toolTaskState.Status = ToolTaskStatus.HostRunning;
                                    break;
                                }
                            case ToolTaskStatus.HostRunning:
                                {
                                    // check uba action state result
                                    int status = GetStatus(toolTaskState.Task);
                                    if (status == 0)
                                    {
                                        toolTaskState.Status = ToolTaskStatus.HostRanToCompletion;
                                    }
                                    else if (status == 1)
                                    {
                                        toolTaskState.Status = ToolTaskStatus.HostFaulted;
                                    }
                                    break;
                                }
                            case ToolTaskStatus.HostRanToCompletion:
                            case ToolTaskStatus.HostFaulted:
                                {
                                    HostSetToolTaskStatus(buildId, toolTaskId, (uint)toolTaskState.Status);
                                    _toolTasks.Remove(toolTaskId);
                                    break;
                                }
                        }
                    }
                }

                return devenv.ExitCode;
            });
        }

        private void DelegateToUba(ToolTask toolTask)
        {
        }

        private int GetStatus(ToolTask toolTask)
        {
            return 0;
        }
    }
}
