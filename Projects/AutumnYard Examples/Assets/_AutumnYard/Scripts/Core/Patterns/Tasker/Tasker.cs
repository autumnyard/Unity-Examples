using System.Collections.Generic;
using UnityEngine;

namespace AutumnYard.Tools.Tasker
{
    // Usage:
    //
    //  var startTask = new StartTask();
    //  var authenticateWorker = new AuthenticateWorkerTask(payload.SessionTicket);
    //  var createContext = new CreateWorkerAuthenticationContext(authenticateWorker.UserAccountInfo, payload.SessionTicket);
    //  var sendResponse = new SendAuthenticationResponse(World.GetExistingSystem<CommandSystem>(), request.RequestId, true);
    //
    //  startTask.NextTasks = new[] {authenticateWorker};
    //  authenticateWorker.NextTasks = new[] { createContext };
    //  createContext.NextTasks = new[] { sendResponse };
    //
    //  Tasker runner = new Tasker();
    //  runner.Add(startTask);
    //  runner.Update();

    public sealed class Tasker
    {
        private List<ITask> tasks = new List<ITask>();

        public void Enqueue(ITask task) => tasks.Add(task);
        public void Insert(ITask task) => tasks.Insert(1, task);
        public void Remove(ITask task) => tasks.Remove(task);

        public void ClearTypes<T>()
        {
            while (tasks.Count > 0)
            {
                if (tasks[0] is T)
                {
                    tasks.RemoveAt(0);
                    continue;
                }

                return;
            }
        }

        public void Update()
        {
            if (tasks.Count <= 0) return;

            var task = tasks[0];

            if (!task.IsRunning)
            {
                Debug.Log("Starting Task: " + task.GetType().Name);

                task.Start();
                task.IsRunning = true;
            }

            if (task.IsRunning)
            {
                task.Update();
            }

            if (task.Error)
            {
                Debug.LogError($"Error in task {task.GetType()}");
            }

            if (task.IsDone)
            {
                Debug.Log("Finished Task: " + task.GetType().Name);

                if (task.NextTasks != null)
                {
                    tasks.AddRange(task.NextTasks);
                }
                tasks.RemoveAt(0);
            }
        }
    }

    public sealed class StartTask : ITask
    {
        public IEnumerable<ITask> NextTasks { get; set; }
        public bool IsDone { get; set; }
        public bool IsRunning { get; set; }
        public bool Error { get; set; }

        public void Start()
        {
            IsDone = true;
        }

        public void Update()
        {

        }
    }
}
