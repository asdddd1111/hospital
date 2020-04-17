namespace NewAmsterdamHospital.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using NewAmsterdamHospital.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<NewAmsterdamHospital.Models.UserContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "NewAmsterdamHospital.Models.UserContext";
        }

        protected override void Seed(NewAmsterdamHospital.Models.UserContext db)
        {
            db.Roles.Add(new Role { Id = 1, Name = "admin" });
            db.Roles.Add(new Role { Id = 2, Name = "user" });
            db.Roles.Add(new Role { Id = 3, Name = "doctor" });
            db.SaveChanges();
            db.DataForContacts.Add(new DataForContact { Address = "462 First Avenue, New York  ", PhoneNumber = "P: 1-212-562-4141", WorkDaysWeek = "Mon-Fris ", StartWorkTime = new DateTime(2020, 03, 30, 8, 0, 0), EndWorkTime = new DateTime(2020, 03, 30, 16, 0, 0) });
            db.SaveChanges();
            db.NumberRooms.Add(new NumberRoom { Id = 1, Number = 1, IsItUsed = true });
            for (int i = 2; i < 10; i++)
            {
                db.NumberRooms.Add(new NumberRoom { Id = i, Number = i, IsItUsed = false });
            }
            db.SaveChanges();
            db.DoctorSpecialties.Add(new DoctorSpecialty { Id = 1, Name = "ALLERGY", NumberRoomId = 1 });
            db.SaveChanges();
            db.Users.Add(new User
            {
                Name = "Name",
                Surname = "Surname",
                DateOfBirth = new DateTime(2020, 1, 1, 0, 0, 0),
                PhoneNumber = "9110",
                Email = "admin@gmail.com",
                Password = "123456",
                RoleId = 1
            }
            );
            db.Users.Add(new User
            {
                Name = "Name1",
                Surname = "Surname1",
                DateOfBirth = new DateTime(1994, 1, 1, 0, 0, 0),
                PhoneNumber = "313",
                Email = "ad@gmail.com",
                Password = "123456",
                RoleId = 2
            }
            );
            db.Users.Add(new User
            {
                Name = "Mark",
                Surname = "Som",
                Email = "som@gamil.com",
                Password = "123456",
                DoctorSpecialtyId = 1,
                DateOfBirth = new DateTime(1999, 12, 12, 0, 0, 0),
                RoleId = 3,
                PhoneNumber = "9111"


            });
            db.SaveChanges();
            int timeOfAppointment = 30;
            int dayOfWork = 1;
            int daysInWeek = 7;

            var date = DateTime.Now;
            var twoWeeks = date.AddDays(daysInWeek);
            int beginCountDate = 1;
            int sunday = 0;
            int saturday = 6;
            while (twoWeeks > date)
            {
                var beginTime = db.DataForContacts.FirstOrDefault(x => x.Id == 1).StartWorkTime;
                var endTime = beginTime.AddMinutes(timeOfAppointment);
                if ((int)date.DayOfWeek != sunday & (int)date.DayOfWeek != saturday)
                {
                    while (endTime <= db.DataForContacts.FirstOrDefault(x => x.Id == 1).EndWorkTime)
                    {
                        db.Receptions.Add(new Reception { TimeName = $"{beginTime.ToString("HH:mm")}-{endTime.ToString("HH:mm")}", DateReceptionId = beginCountDate, IsUse = false });
                        beginTime = beginTime.AddMinutes(timeOfAppointment);
                        endTime = endTime.AddMinutes(timeOfAppointment);
                    }

                    db.DateReceptions.Add(new DateReception { Id = beginCountDate, DateName = date.ToString("dd MMMM"), DoctorSpecialtyId = 1, Date = date });
                }
                date = date.AddDays(dayOfWork);
                beginCountDate++;
            }
            db.SaveChanges();

            db.DataForCarousels.Add(new DataForCarousel { Class = "item active", UrlPicture = "https://www.watts.com/dfsmedia/0533dbba17714b1ab581ab07a4cbb521/33196-50060/hc-t2-mediagrid-img1", TextHeading = "Children's Health Services", Text = "The Children’s Health Center provides routine exams, immunizations, child development guidance, and more. Also available are a range of specialty pediatric services including pediatric cardiology, neurology, and treatment for asthma and allergies." });
            db.DataForCarousels.Add(new DataForCarousel { Class = "item", UrlPicture = "http://fhmedical.ca/wp-content/uploads/2018/07/healthy-heart-banner-final.jpg", TextHeading = "Heart Health Program", Text = " New Amsterdam Hospital and Cardiothoracic Surgery Departments are world - renowned.We provide a wide range of outpatient and inpatient diagnostic, clinical, and surgical services for cardiology patients, and we are the only public hospital in New York City where open-heart surgery is performed." });
            db.DataForCarousels.Add(new DataForCarousel { Class = "item", UrlPicture = "https://www.scvmc.org/health-care-services/Womens-Health/Pregnancy-and-Birth/PublishingImages/womens-health-labor-and-delivery.jpg", TextHeading = "Labor and Delivery", Text = "New Amsterdam Hospital has been attending to the health needs and medical care of women for nearly three centuries.As a woman’s health needs change at different times in her life, our Obstetrics and Gynecology(OB - GYN) service provides excellent care at each of these stages." });
            db.SaveChanges();

            db.DataForNews.Add(new DataForNew { TextHeading = " Mayor Announces Outposted Therapeutic Housing Units to Serve Patients in Custody with Serious Health Needs", Text = "Units at Bellevue and Woodhull build on City’s criminal justice reform strategy." });
            db.DataForNews.Add(new DataForNew { TextHeading = " 16 Nurse Professionals Honored as Part of Annual Nursing Excellence Awards", Text = " Honorees highlight the tireless work nurses do for patients." });
            db.DataForNews.Add(new DataForNew { TextHeading = " Gotham Health, Judson Opens New Pride Health Center for LGBTQ Youth", Text = "Center joins the public health system's existing four Pride Health Centers." });
            db.SaveChanges();
        }
    }
}
