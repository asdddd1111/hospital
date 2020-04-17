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
        readonly long interval = 604800000;
        readonly object synclock = new object();
        static bool sent = false;
        readonly int saturday = 6;
        public void Init(HttpApplication app)
        {
            timer = new Timer(new TimerCallback(ChangeRecords), null, 0, interval);
        }
        private void ChangeRecords(object obj)
        {
            lock (synclock)
            {
                DateTime dd = DateTime.Now;
                if ((int)dd.DayOfWeek== saturday && sent == false)
                {
                    Models.UserContext db = new Models.UserContext();
                    foreach (var item in db.DoctorSpecialties)
                    {
                        Service.ScheduleForDoctor.Add(item.Id,db.DateReceptions.Where(x=>x.DoctorSpecialtyId==item.Id).Max(y=>y.Date));
                    }
                    
                    sent = true;
                }
                else if ((int)dd.DayOfWeek != saturday)
                {
                    sent = false;
                }
            }
        }
        public void Dispose()
        { }
    }
}