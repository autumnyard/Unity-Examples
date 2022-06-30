using System.Collections.Generic;

namespace AutumnYard.Tools.Tasker
{
    public interface ITask
    {
        IEnumerable<ITask> NextTasks { get; set; }

        bool IsDone { get; set; }
        bool IsRunning { get; set; }
        bool Error { get; set; }

        void Start();
        void Update();
    }
}
