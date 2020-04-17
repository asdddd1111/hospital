using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewAmsterdamHospital.ViewModels
{
    public class ViewModelForUserIndex
    {
        public DataUser DataUser { get; set; }
        public IEnumerable<ListAppointmentForUser> Lists { get; set; }
    }
}