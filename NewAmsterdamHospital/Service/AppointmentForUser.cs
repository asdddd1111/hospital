using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewAmsterdamHospital.Service
{
    public static class AppointmentForUser
    {
        public static List<ViewModels.ListAppointmentForUser> GetList(string email)
        {
            List<ViewModels.ListAppointmentForUser> appointment = new List<ViewModels.ListAppointmentForUser>();
            Models.UserContext db = new Models.UserContext();
            int index = 1;
            foreach (var list in db.Appoitments.Where(x => x.EmailUser == email))
            {
                if (db.DateReceptions.FirstOrDefault(x => x.Id == list.DateId).Date.Day >= DateTime.Now.Day)
                {
                    appointment.Add(new ViewModels.ListAppointmentForUser { Id = index, NameDoctorSpecialty = db.DoctorSpecialties.FirstOrDefault(x => x.Id == list.IdDoctorSpecialty).Name, DateName = db.DateReceptions.FirstOrDefault(x => x.Id == list.DateId).DateName, TimeName = db.Receptions.FirstOrDefault(x => x.Id == list.TimeId).TimeName, NumberRoom = db.DoctorSpecialties.FirstOrDefault(x => x.Id == list.IdDoctorSpecialty).NumberRoomId, IdAppoitment = list.Id });
                    index++;
                }
            }
            return appointment;
        }
    }
}