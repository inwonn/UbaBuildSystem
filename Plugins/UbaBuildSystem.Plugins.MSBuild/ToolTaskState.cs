using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UbaBuildSystem.Plugins.MSBuild.Generated;

namespace UbaBuildSystem.Plugins.MSBuild
{
    internal class ToolTaskState
    {
        public ToolTask Task { get; set; }
        public ToolTaskStatus Status { get; set; }
        public ToolTaskState[]? TaskStates { get; set; }

        public ToolTaskState(ToolTask task, ToolTaskStatus status)
        {
            Task = task;
            Status = status;
            TaskStates = task.GenerateSubTaskStates();
        }

        public bool IsRunning()
        {
            if (TaskStates == null)
                return Status == ToolTaskStatus.HostRunning;
            
            return TaskStates.All(t => t.IsRunning());
        }

        public bool IsCompleted()
        {
            if (TaskStates == null)
                return Status == ToolTaskStatus.HostRanToCompletion;
            
            return TaskStates.All(t => t.IsCompleted());
        }

        public bool IsFaulted()
        {
            if (TaskStates == null)
                return Status == ToolTaskStatus.HostFaulted;
            
            return TaskStates.All(t => t.IsFaulted());
        }
    }
}
