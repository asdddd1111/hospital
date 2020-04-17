using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewAmsterdamHospital.Models;

namespace NewAmsterdamHospital.Service
{
    public static class ScheduleForDoctor
    {
        public static void Add( int indexForDoctorSpecialtie, DateTime date)
        {
            UserContext db = new UserContext();
            if (db.Receptions.Count() == 0)
            {
                int timeOfAppointment = 30;
                int dayOfWork = 1;
                int daysInWeek = 7;
                var twoWeeks = date.AddDays(daysInWeek);
                int sunday = 0;
                int saturday = 6;
                int idDateReception = 1;
                while (twoWeeks >=date)
                {
                    var beginTime = db.DataForContacts.FirstOrDefault(x => x.Id == 1).StartWorkTime;
                    var endTime = beginTime.AddMinutes(timeOfAppointment);
                    if ((int)date.DayOfWeek != sunday & (int)date.DayOfWeek != saturday)
                    {
                        while (endTime <= db.DataForContacts.FirstOrDefault(x => x.Id == 1).EndWorkTime)
                        {
                            db.Receptions.Add(new Reception { TimeName = $"{beginTime.ToString("HH:mm")}-{endTime.ToString("HH:mm")}", DateReceptionId = idDateReception, IsUse = false });
                            beginTime = beginTime.AddMinutes(timeOfAppointment);
                            endTime = endTime.AddMinutes(timeOfAppointment);
                        }
                        db.DateReceptions.Add(new DateReception { Id = idDateReception, DateName = date.ToString("dd MMMM"), DoctorSpecialtyId = indexForDoctorSpecialtie, Date = date });
                    }
                    date = date.AddDays(dayOfWork);
                    idDateReception++;
                }
                db.SaveChanges();
            }
            else
            {
                int timeOfAppointment = 30;
                int dayOfWork = 1;
                int daysInWeek = 7;
                var twoWeeks = date.AddDays(daysInWeek);
                int sunday = 0;
                int saturday = 6;
                int idDateReception = db.DateReceptions.Max(p => p.Id) + 1;
                while (twoWeeks >= date)
                {
                    var beginTime = db.DataForContacts.FirstOrDefault(x => x.Id == 1).StartWorkTime;
                    var endTime = beginTime.AddMinutes(timeOfAppointment);
                    if ((int)date.DayOfWeek != sunday & (int)date.DayOfWeek != saturday)
                    {
                        while (endTime <= db.DataForContacts.FirstOrDefault(x => x.Id == 1).EndWorkTime)
                        {
                            db.Receptions.Add(new Reception { TimeName = $"{beginTime.ToString("HH:mm")}-{endTime.ToString("HH:mm")}", DateReceptionId = idDateReception, IsUse = false });
                            beginTime = beginTime.AddMinutes(timeOfAppointment);
                            endTime = endTime.AddMinutes(timeOfAppointment);
                        }
                        db.DateReceptions.Add(new DateReception { Id = idDateReception, DateName = date.ToString("dd MMMM"), DoctorSpecialtyId = indexForDoctorSpecialtie, Date = date });
                    }
                    date = date.AddDays(dayOfWork);
                    idDateReception++;
                }
                db.SaveChanges();
            }
            
        }
    }
}