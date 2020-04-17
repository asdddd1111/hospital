using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewAmsterdamHospital.Models
{
    public class DoctorSpecialty
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberRoomId { get; set; }
        public NumberRoom NumberRoom { get; set; }
        public ICollection<DateReception> DateReceptions { get; set; }
    }
}