using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NewAmsterdamHospital.ViewModels
{
    public class DataUser
    {
        [Required(ErrorMessage = "Enter your name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter your surname")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Enter your date of birth")]
        [DataType(DataType.Date)]
        [Date(ErrorMessage = "Date of birth can be since 1910 till now ")]
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Enter your phone number")]
        public string PhoneNumber { get; set; }
    }
}