using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewAmsterdamHospital.Models;

namespace NewAmsterdamHospital.ViewModels
{
    public class DataForMainIndex
    {
        public IEnumerable<DataForCarousel> Carousels { get; set; }
        public IEnumerable<DataForNew> News { get; set; }
    }
}