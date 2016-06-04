using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebChallengeFinal.Models;
using WebChallengeFinal.Services;
using System.Web.Mvc;
using Jint;

namespace WebChallengeFinal.Controllers
{
    public class SchedulerController : Controller
    {
        private ScheduleService _scheduler = SchedulerSingelton.GetInstance(100);
        public int AddTask(string code, int priority)
        {
            if (priority < 0 && priority > 100)
                return -3;

            ScheduleTask task = new ScheduleTask(code, priority);
            
            _scheduler.AddTask(task);

            return 0;
        }
    }
}
