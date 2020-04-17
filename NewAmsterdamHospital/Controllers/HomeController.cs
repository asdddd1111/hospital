using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace NewAmsterdamHospital.Controllers
{
   
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Models.UserContext db = new Models.UserContext();
            ViewModels.DataForMainIndex dataForMainIndex = new ViewModels.DataForMainIndex() { Carousels = db.DataForCarousels, News = db.DataForNews };
            return View(dataForMainIndex);
        }
       
        public ActionResult About()
        {
            int idRoleDoctor = 3;
            Models.UserContext db = new Models.UserContext();
            List<ViewModels.ListDoctor> listDoctors = new List<ViewModels.ListDoctor>();
            foreach (var list in db.Users.Include(y=>y.DoctorSpecialty).Where(x => x.RoleId == idRoleDoctor))
            {
                listDoctors.Add(new ViewModels.ListDoctor {Specialty=list.DoctorSpecialty.Name ,Name=list.Name, Surname=list.Surname });
            }
           
            return View(listDoctors);
        }

        public ActionResult Contact()
        {
            Models.UserContext db = new Models.UserContext();
            ViewModels.DataForContactView contactView = new ViewModels.DataForContactView() { Address = db.DataForContacts.FirstOrDefault(x => x.Id == 1).Address, PhoneNumber = db.DataForContacts.FirstOrDefault(x => x.Id == 1).PhoneNumber, WorkDaysWeek = db.DataForContacts.FirstOrDefault(x => x.Id == 1).WorkDaysWeek, StartWorkTime= db.DataForContacts.FirstOrDefault(x => x.Id == 1).StartWorkTime, EndWorkTime= db.DataForContacts.FirstOrDefault(x => x.Id == 1).EndWorkTime };

            return View(contactView);
        }
    }
}