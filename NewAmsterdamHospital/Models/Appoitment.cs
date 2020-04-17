using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace NewAmsterdamHospital.Models
{
    public class Appoitment
    {
        public int Id { get; set; }
        public string  EmailUser { get; set; }
        public int IdDoctorSpecialty { get; set; }
        public int DateId { get; set; }
        public int TimeId { get; set; }
    }
}