using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NewAmsterdamHospital.ViewModels
{
    public class AppointmentView
    {
        public int IdDoctorSpecialty { get; set; }
        public int DateId { get; set; }
        public int TimeId { get; set; }
    }
}