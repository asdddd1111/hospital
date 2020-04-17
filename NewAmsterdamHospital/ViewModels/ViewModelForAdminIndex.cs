using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewAmsterdamHospital.ViewModels
{
    public class ViewModelForAdminIndex
    {
        public IEnumerable<DataCustomerForAdmin> ListCustomer { get; set; }
        public IEnumerable<DataDoctorForAdmin> ListDoctor { get; set; }
    }
}