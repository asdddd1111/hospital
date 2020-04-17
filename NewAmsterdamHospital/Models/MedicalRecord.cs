using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NewAmsterdamHospital.Models
{
    public class MedicalRecord
    {
        public int Id { get; set; }
        [Display(Name = "Doctor Specialty")]
        public string DoctorSpecialty { get; set; }
        [Display(Name = "Email Patient")]
        public string EmailCustomer { get; set; }
        [Display(Name = "Date Of Change")]
        public DateTime DateOfChange { get; set; }
        public string Record { get; set; } 
    }
}