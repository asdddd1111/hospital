using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NewAmsterdamHospital.Models
{
    public class DataForContact
    {
        public int Id { get; set; } 
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string WorkDaysWeek { get; set; }
        public DateTime StartWorkTime { get; set; }
        public DateTime EndWorkTime { get; set; }
    }
}