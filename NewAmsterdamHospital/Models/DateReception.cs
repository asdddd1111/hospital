using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewAmsterdamHospital.Models
{
    public class DateReception
    {
        public int Id { get; set; }
        public string DateName { get; set; }
        public DateTime Date { get; set; }
        public ICollection<Reception> Receptions { get; set; }

        public int DoctorSpecialtyId { get; set; }
        public DoctorSpecialty DoctorSpecialty { get; set; }
    }
}