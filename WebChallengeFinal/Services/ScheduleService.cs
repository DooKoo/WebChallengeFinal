using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebChallengeFinal.Models;
using System.Threading;
using System.Collections.Concurrent;

namespace WebChallengeFinal.Services
{
    public class ScheduleService
    {
        private List<ScheduleTask> _queue { get; set; }
        private Thread scheduleMonitor { get; set; }

        public ScheduleService(int time)
        {
            _queue = new List<ScheduleTask>();
            scheduleMonitor = new Thread(() =>
            {
                while (true)
                {
                    _queue.Where(t => t.Status != EScheduleTaskStatus.Finished).ToList().OrderBy(t => t.Priority)
                    .ToList().ForEach(task =>
                    {
                        System.Diagnostics.Debug.WriteLine("Started task with code:" + task.Code);
                        task.Run();
                        Thread.Sleep(time);
                        task.Suspend();
                        System.Diagnostics.Debug.WriteLine("Stopped task with code:" + task.Code);
                    });
                }
            });
            scheduleMonitor.Start();               
        }

        public void AddTask(ScheduleTask task)
        {
            _queue.Add(task);
            int a = 5;
        }

        public void Restart()
        {
            scheduleMonitor.Abort();
            scheduleMonitor.Start();
        }
    }
}