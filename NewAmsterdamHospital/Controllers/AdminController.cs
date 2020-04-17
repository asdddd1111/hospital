using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using NewAmsterdamHospital.Models;


namespace NewAmsterdamHospital.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {

        public ActionResult Index()
        {
            UserContext db = new UserContext();
            ViewModels.NumberOfUsers numberOfUsers = new ViewModels.NumberOfUsers
            {
                NumberOfCustomer = db.Users.Where(x => x.RoleId == 2).Count(),
                NumberOfDoctor = db.Users.Where(x => x.RoleId == 3).Count()
            };
            if (Request.IsAjaxRequest())
            {
                return PartialView("_numberOfUserPartial", numberOfUsers);
            }
           
            return View(numberOfUsers);
        }

        public ActionResult ShowListUser()
        {
            var listCustomer = Service.ListUsersCustomer.GetList();
            var listDoctor = Service.ListUsersDoctor.GetList();
            ViewModels.ViewModelForAdminIndex lists = new ViewModels.ViewModelForAdminIndex { ListCustomer = listCustomer, ListDoctor = listDoctor };
            return View(lists);
        }

        public ActionResult AddNewDoctor()
        {
            UserContext db = new UserContext();
            int selectedIndex = 0;
            SelectList number = new SelectList(db.NumberRooms.Where(x=>x.IsItUsed==false), "Id", "Number", selectedIndex);
            ViewBag.Number = number;
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddNewDoctor(ViewModels.AddNewDoctor newUser)
        {
            UserContext db = new UserContext();
            int selectedIndex = 0;
            SelectList number = new SelectList(db.NumberRooms.Where(x => x.IsItUsed == false), "Id", "Number", selectedIndex);
            ViewBag.Number = number;
            int idDoctorRole = 3;
            var user = db.Users.FirstOrDefault(x => x.Email == newUser.Email);
            if (ModelState.IsValid)
            {
                if (user == null)
                {
                    var doctor = db.DoctorSpecialties.FirstOrDefault(x => x.Name == newUser.DoctorSpecialty.ToUpper());
                    if ( doctor == null)
                    {
                        db.DoctorSpecialties.Add(new DoctorSpecialty {  Name = newUser.DoctorSpecialty.ToUpper(), NumberRoomId = newUser.NumberRoomId });
                        db.SaveChanges();
                        db.Users.Add(new User { Name = newUser.Name, Surname = newUser.Surname, DateOfBirth = newUser.DateOfBirth, Email = newUser.Email, Password = newUser.Password, PhoneNumber = newUser.PhoneNumber, RoleId = idDoctorRole, DoctorSpecialtyId = db.DoctorSpecialties.FirstOrDefault(x => x.Name == newUser.DoctorSpecialty.ToUpper()).Id });
                        db.SaveChanges();
                        Service.ScheduleForDoctor.Add(db.DoctorSpecialties.FirstOrDefault(x=>x.Name==newUser.DoctorSpecialty.ToUpper()).Id,DateTime.Now);
                        db.NumberRooms.FirstOrDefault(x => x.Id == newUser.NumberRoomId).IsItUsed = true;
                        db.SaveChanges();
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                            ModelState.AddModelError("", "Such doctor already exists");
                            return View(newUser);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Email is already registered");
                    return View(newUser);
                }
            }
            return View(newUser);
        }
        public ActionResult DeleteCustomer(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult DeleteCustomer(int id, FormCollection collection)
        {
            try
            {
                UserContext db = new UserContext();
                var userForDelete = db.Users.FirstOrDefault(x=>x.Id==id);
                var emailDeleteUser = db.Users.FirstOrDefault(x => x.Id == id).Email;
                var deleteAppoitment = db.Appoitments.Where(x => x.EmailUser == emailDeleteUser);
                foreach (var list in deleteAppoitment)
                {
                    db.Appoitments.Remove(list);
                    db.Receptions.FirstOrDefault(x => x.Id == list.TimeId).IsUse = false;
                }

                db.Users.Remove(userForDelete);
                db.SaveChanges();

                if (Request.IsAjaxRequest())
                {
                    return Json(new { customerId = id });
                }

                return RedirectToAction("ShowListUser");
            }
            catch (Exception e)
            {
                if (Request.IsAjaxRequest())
                {
                    return Json(new { error = e.Message });
                }

                return View();
            }
        }
        public ActionResult DeleteDoctor(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult DeleteDoctor(int id, FormCollection collection)
        {
            try
            {
                UserContext db = new UserContext();
                var userForDelete = db.Users.FirstOrDefault(x => x.Id == id);
                var doctorSpecialtyIdForDelete = db.Users.FirstOrDefault(x => x.Id == id).DoctorSpecialtyId;
                foreach (var list in db.Appoitments.Where(x => x.IdDoctorSpecialty == doctorSpecialtyIdForDelete))
                {
                    db.Appoitments.Remove(list);
                }
                
                foreach (var list in db.DateReceptions.Where(x => x.DoctorSpecialtyId == doctorSpecialtyIdForDelete))
                {
                    foreach (var nextList in db.Receptions.Where(x => x.DateReceptionId==list.Id))
                    {
                        db.Receptions.Remove(nextList);
                    }
                    db.DateReceptions.Remove(list);
                }
                db.NumberRooms.FirstOrDefault(x => x.Id == db.DoctorSpecialties.FirstOrDefault(y => y.Id == doctorSpecialtyIdForDelete).NumberRoomId).IsItUsed = false;
                db.DoctorSpecialties.Remove(db.DoctorSpecialties.FirstOrDefault(x => x.Id == doctorSpecialtyIdForDelete));
                db.Users.Remove(userForDelete);
                db.SaveChanges();

                if (Request.IsAjaxRequest())
                {
                    return Json(new { doctorId = id });
                }

                return RedirectToAction("ShowListUser");
            }
            catch (Exception e)
            {
                if (Request.IsAjaxRequest())
                {
                    return Json(new { error = e.Message });
                }

                return View();
            }
        }
        public ActionResult RedactMainPage()
        {
            UserContext db = new UserContext();
            int selectedIndex = 0;
            SelectList numberCarousel = new SelectList(db.DataForCarousels, "Id", "Id", selectedIndex);
            ViewBag.NumberCarousel = numberCarousel;
            SelectList numberNew = new SelectList(db.DataForNews, "Id", "Id", selectedIndex);
            ViewBag.NumberNew = numberNew;
            return View();
        }
        [HttpPost]
        public ActionResult RedactCarousel(ViewModels.DataForMainPageView content)
        {
            UserContext db = new UserContext();
            db.DataForCarousels.FirstOrDefault(x => x.Id == content.DataForCarousel.Id).UrlPicture = content.DataForCarousel.UrlPicture;
            db.DataForCarousels.FirstOrDefault(x => x.Id == content.DataForCarousel.Id).TextHeading = content.DataForCarousel.TextHeading;
            db.DataForCarousels.FirstOrDefault(x => x.Id == content.DataForCarousel.Id).Text = content.DataForCarousel.Text;
            db.SaveChanges();
            return RedirectToAction("RedactMainPage", "Admin");
        }
        public ActionResult RedactNew(ViewModels.DataForMainPageView content)
        {
            UserContext db = new UserContext();
            db.DataForNews.FirstOrDefault(x => x.Id == content.DataForNews.Id).TextHeading = content.DataForNews.TextHeading;
            db.DataForNews.FirstOrDefault(x => x.Id == content.DataForNews.Id).Text = content.DataForNews.Text;
            db.SaveChanges();
            return RedirectToAction("RedactMainPage", "Admin");
        }
        public ActionResult RedactContact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RedactContact(ViewModels.DataForContactView contact)
        {
            UserContext db = new UserContext();
            int idForContact = 1;
            db.DataForContacts.FirstOrDefault(x => x.Id == idForContact).Address = contact.Address;
            db.DataForContacts.FirstOrDefault(x => x.Id == idForContact).PhoneNumber = contact.PhoneNumber;
            db.DataForContacts.FirstOrDefault(x => x.Id == idForContact).WorkDaysWeek = contact.WorkDaysWeek;
            db.DataForContacts.FirstOrDefault(x => x.Id == idForContact).StartWorkTime = contact.StartWorkTime;
            db.DataForContacts.FirstOrDefault(x => x.Id == idForContact).EndWorkTime = contact.EndWorkTime;
            db.SaveChanges();
            return RedirectToAction("Index", "Admin");
        }
    }
}