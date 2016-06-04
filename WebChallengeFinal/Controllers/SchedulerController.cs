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
using System.Web.Script.Serialization;

namespace WebChallengeFinal.Controllers
{
    public class SchedulerController : Controller
    {
        private ScheduleService _scheduler = SchedulerSingelton.GetInstance(5);
        public int AddTask(string code="", int priority=0)
        {
            if (priority < 0 || priority > 100)
                return -3;

            if (code.Length == 0)
                return -1;

            ScheduleTask task = new ScheduleTask(code, priority);
            
            _scheduler.AddTask(task);

            return 0;
        }

        public string GetInfo()
        {
            var queue = _scheduler.Queue.ToList();
            return new JavaScriptSerializer().Serialize(queue.Select(i => new { Priority = i.Priority, Code = i.Code, Status = StatusToString(i.Status) }));
        }

        #region Util
        private string StatusToString(EScheduleTaskStatus status)
        {
            switch (status)
            {
                case EScheduleTaskStatus.Blocked:
                    return "Заблоковано";
                case EScheduleTaskStatus.Finished:
                    return "Виконано";
                case EScheduleTaskStatus.Ready:
                    return "Готово до виконання";
                case EScheduleTaskStatus.Running:
                    return "Виконується";
                default:
                    return "Щось пішло не так :(";
            }
        }
        #endregion
    }
}
