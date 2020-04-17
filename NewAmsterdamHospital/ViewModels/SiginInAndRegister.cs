using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NewAmsterdamHospital.ViewModels
{
    public class SiginIn
    {   
        [Display(Name ="Your Email")]
        [Required(ErrorMessage ="Enter your email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Your Password")]
        [Required(ErrorMessage = "Enter your password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
    public class Register
    {
        [Required(ErrorMessage = "Enter your name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter your surname")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Enter your password")]
        [DataType(DataType.Password)]
        [StringLength(20),MinLength(6)]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Enter your password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Enter your email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter your date of birth")]
        [DataType(DataType.Date)]
        [Date(ErrorMessage = "Date of birth can be since 1910 till now ")]
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Enter your phone number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

    }
    public class DateAttribute : RequiredAttribute
    {
        public override bool IsValid(object value)
        {
            var date = new DateTime(1910, 1, 1, 0, 0, 0);
            return base.IsValid(value) && ((DateTime)value) < DateTime.Now && ((DateTime)value) > date;
        }
    }
}