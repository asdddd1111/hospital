using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewAmsterdamHospital.Models
{
    public class DataForCarousel
    {
        public int Id { get; set; }
        public string Class { get; set; }
        public string TextHeading { get; set; }
        public string Text { get; set; }
        public string UrlPicture { get; set; }
    }
}