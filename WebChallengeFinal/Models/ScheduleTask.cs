using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using Jint;

namespace WebChallengeFinal.Models
{
    public class ScheduleTask
    {
        public ScheduleTask(string code, int priority)
        {
            Status = EScheduleTaskStatus.Ready;
            Code = code;
            Priority = priority;
        }

        /// <summary>
        /// Task priority from 0 to 100
        /// 0 - minimal priority, 100 maximal priority
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// Code that will be running
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Status of the task
        /// </summary>
        public EScheduleTaskStatus Status { get; private set; }

        /// <summary>
        /// Thread for task execution
        /// </summary>
        private Thread _executionThread { get; set; }

        public void Run()
        {
            if (Status == EScheduleTaskStatus.Ready)
            {
                _executionThread = new Thread(() =>
                {
                    var jintEngine = new Engine();
                    try
                    {
                        jintEngine.Execute(Code);
                    }
                    catch (Exception e)
                    {
                        System.Diagnostics.Debug.WriteLine(e.Message);
                        Status = EScheduleTaskStatus.Failed;
                    }
                });

                Status = EScheduleTaskStatus.Running;
                _executionThread.Start();
            }
            else if(Status == EScheduleTaskStatus.Blocked)
            {
                Status = EScheduleTaskStatus.Running;
                _executionThread.Resume();
            }
        }

        public void Suspend()
        {
            if (!_executionThread.IsAlive)
            {
                if(Status != EScheduleTaskStatus.Failed)
                    Status = EScheduleTaskStatus.Finished;
            }

            if (Status == EScheduleTaskStatus.Running)
            {                
                Status = EScheduleTaskStatus.Blocked;
                _executionThread.Suspend();
            }
        }
    }

    public enum EScheduleTaskStatus
    {
        Ready,
        Running,
        Blocked,
        Finished,
        Failed        
    }
}