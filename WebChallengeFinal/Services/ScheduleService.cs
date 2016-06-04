using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebChallengeFinal.Models;
using System.Threading;
using System.Collections.Concurrent;
using System.Security.Permissions;

namespace WebChallengeFinal.Services
{
    public class ScheduleService
    {
        public List<ScheduleTask> Queue { get; set; }
        private Thread _scheduleMonitor { get; set; }

        public ScheduleService(int time)
        {
            Queue = new List<ScheduleTask>();
            _scheduleMonitor = new Thread(() =>
            {
                while (true)
                {
                    Queue.ToList().Where(t => t.Status != EScheduleTaskStatus.Finished).ToList().OrderBy(t => t.Priority)
                    .ToList().ForEach(task =>
                    {
                        System.Diagnostics.Debug.WriteLine("Started task with code:" + task.Code);
                        task.Run();
                        Thread.Sleep(time * task.Priority);
                        task.Suspend();
                        System.Diagnostics.Debug.WriteLine("Stopped task with code:" + task.Code);
                    });
                }
            });
            _scheduleMonitor.Start();               
        }

        public void AddTask(ScheduleTask task)
        {            
            Queue.Add(task);
        }

        public void Restart()
        {
            scheduleMonitorAbort();
            _scheduleMonitor.Start();
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, ControlThread = true)]
        private void scheduleMonitorAbort()
        {
            _scheduleMonitor.Abort();
        }
    }
}