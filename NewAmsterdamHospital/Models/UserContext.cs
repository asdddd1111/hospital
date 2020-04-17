using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace NewAmsterdamHospital.Models
{
    public class UserContext: DbContext
    {
        public UserContext() : base("DefaultConnection")
        { }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Reception> Receptions { get; set; }
        public DbSet<DoctorSpecialty> DoctorSpecialties { get; set; }
        public DbSet<Appoitment> Appoitments { get; set; }
        public DbSet<DateReception> DateReceptions { get; set; }
        public DbSet<NumberRoom> NumberRooms { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<DataForCarousel> DataForCarousels { get; set; }
        public DbSet<DataForNew> DataForNews { get; set; }
        public DbSet<DataForContact> DataForContacts { get; set; }
    }
}