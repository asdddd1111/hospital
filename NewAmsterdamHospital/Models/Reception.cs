using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewAmsterdamHospital.Models
{
    public class  Reception
    {
        public int Id { get; set; }
        public string TimeName { get; set; }
        public int DateReceptionId { get; set; }
        public DateReception DateReception { get; set; }
        public bool IsUse { get; set; }
    }
}