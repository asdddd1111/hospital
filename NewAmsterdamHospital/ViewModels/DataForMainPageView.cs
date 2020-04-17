using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewAmsterdamHospital.ViewModels
{
    public class DataForMainPageView
    {
        public DataForCarouselView DataForCarousel { get; set; }
        public Models.DataForNew DataForNews { get; set; }
    }
}