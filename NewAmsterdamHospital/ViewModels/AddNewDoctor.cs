using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace NewAmsterdamHospital.ViewModels
{
    public class AddNewDoctor
    {
        [Required(ErrorMessage = "Enter user name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter user surname")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Enter user password")]
        [DataType(DataType.Password)]
        [StringLength(20), MinLength(6)]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Enter user password")]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Enter user email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Date Of Birth")]
        [Required(ErrorMessage = "Enter user date of birth")]
        [DataType(DataType.Date)]
        [Date(ErrorMessage = "Date of birth can be since 1910 till now")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Enter user phone number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Enter user role")]
        public int IdRole { get; set; }

        [Display(Name = "Doctor Specialty")]
        [Required(ErrorMessage = "Enter specialty")]
        public string DoctorSpecialty { get; set; }

        [Display(Name = "Number Room")]
        [Required(ErrorMessage = "Enter number room")]
        public int NumberRoomId { get; set; }
    }
    
}