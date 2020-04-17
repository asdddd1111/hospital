using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NewAmsterdamHospital.ViewModels
{
    public class DataForContactView
    {
        [Required(ErrorMessage = "Enter Address")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Enter Phone Number")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Enter Work Days Week")]
        public string WorkDaysWeek { get; set; }
        [Required(ErrorMessage = "Enter Start Work Time")]
        [DataType(DataType.Time)]
        public DateTime StartWorkTime { get; set; }
        [Required(ErrorMessage = "Enter End Work Time")]
        [DataType(DataType.Time)]
        public DateTime EndWorkTime { get; set; }
    }
}