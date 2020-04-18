using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;


namespace NewAmsterdamHospital.Modules
{
    public class TimerModule : IHttpModule
    {
        static Timer timer;
        long interval = 30000; 
        static object synclock = new object();
        static bool sent = false;

        public void Init(HttpApplication app)
        {
            timer = new Timer(new TimerCallback(SendEmail), null, 0, interval);
        }

        private void SendEmail(object obj)
        {
            lock (synclock)
            {
                DateTime dd = DateTime.Now;
                if (dd.DayOfWeek==DayOfWeek.Saturday&&dd.Hour == 13 && dd.Minute == 20 && sent == false)
                {
                    Models.UserContext db = new Models.UserContext();
                    foreach (var list in db.DoctorSpecialties)
                    {
                        Service.ScheduleForDoctor.Add(list.Id, db.DateReceptions.Where(x => x.DoctorSpecialtyId == list.Id).Max(y => y.Date).AddDays(1));
                    }

                    sent = true;
                }
                else if (dd.DayOfWeek!=DayOfWeek.Saturday &&dd.Hour != 20 && dd.Minute != 0)
                {
                    sent = false;
                }
            }
        }
        public void Dispose()
        { }
    }
}
