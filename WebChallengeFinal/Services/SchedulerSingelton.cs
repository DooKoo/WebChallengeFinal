using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebChallengeFinal.Services
{
    public static class SchedulerSingelton
    {
        private static ScheduleService _scheduler;
        public static ScheduleService GetInstance(int time)
        {
            if(_scheduler == null)
            {
                _scheduler = new ScheduleService(time);
            }

            return _scheduler;
        }
    }
}