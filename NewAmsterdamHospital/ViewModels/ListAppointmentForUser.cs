using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewAmsterdamHospital.ViewModels
{
    public class ListAppointmentForUser
    {
        public int Id { get; set; }
        public string  NameDoctorSpecialty { get; set; }
        public string DateName { get; set; }
        public string TimeName { get; set; }
        public int NumberRoom { get; set; }
        public int IdAppoitment { get; set; }
    }
}