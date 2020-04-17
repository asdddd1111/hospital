using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NewAmsterdamHospital.ViewModels
{
    public class Schedule
    {
        public int Id { get; set; }
        [Display(Name ="Time")]
        public string TimeName { get; set; }
        [Display(Name ="Date")]
        public string DateName { get; set; }
    }
}