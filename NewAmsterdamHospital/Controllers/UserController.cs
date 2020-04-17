using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewAmsterdamHospital.ViewModels;


namespace NewAmsterdamHospital.Controllers
{ 
    [Authorize(Roles = "user")]
    public class UserController : Controller
    {

        public ActionResult Index()
        {
            var email = HttpContext.User.Identity.Name;
            Service.DataForAccount data = new Service.DataForAccount();
            var user = data.GetUser(email);
            var list = Service.AppointmentForUser.GetList(email);
            ViewModelForUserIndex userIndex = new ViewModelForUserIndex {DataUser= user,Lists=list };
            if (Request.IsAjaxRequest())
            {
                return PartialView("_appointmentPartial",userIndex);
            }
            return View("Index",userIndex);
        }
        
        public ActionResult Edit()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DataUser user)
        {
            if (ModelState.IsValid)
            {
                var email = HttpContext.User.Identity.Name;
                Service.DataForEdit data = new Service.DataForEdit();
                data.Edit(email, user);
            }
            return RedirectToAction("Index", "User");
        }
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(DataForChangePassword password)
        {
            var email = HttpContext.User.Identity.Name;
            Service.DataForNewPassword passwordNew = new Service.DataForNewPassword();
            passwordNew.Change(password, email);
            return RedirectToAction("Index", "User");
        }
      
        public ActionResult GetDate(int id)
        {
            Models.UserContext db = new Models.UserContext();
            return PartialView(db.DateReceptions.Where(x => x.DoctorSpecialtyId == id&&x.Date>DateTime.Now));
           

        }
      
        public ActionResult GetTime(int id)
        {
            Models.UserContext db = new Models.UserContext();
            return PartialView(db.Receptions.Where(x => x.DateReceptionId == id && x.IsUse == false).ToList());
        }
        public ActionResult MakeAnAppointment()
        {
           int selectedIndex = -1;
            int selectedIndex1 = -1;
            int selectedIndex2 = -1;
            Models.UserContext db = new Models.UserContext();
            SelectList specialties = new SelectList(db.DoctorSpecialties, "Id", "Name", selectedIndex);
            ViewBag.Specialties = specialties;
            SelectList date = new SelectList(db.DateReceptions.Where(x => x.DoctorSpecialtyId == selectedIndex), "Id", "DateName", selectedIndex1);
            ViewBag.Date = date;
            SelectList time = new SelectList(db.Receptions.Where(x => x.DateReceptionId == selectedIndex1&&x.IsUse==false), "Id", "TimeName", selectedIndex2);
            ViewBag.Time = time;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MakeAnAppointment(AppointmentView appoitment)
        {
            var idTime = Convert.ToInt32(Request.Form["time"]);
            var idDate =Convert.ToInt32( Request.Form["date"]);
            var email = HttpContext.User.Identity.Name;
            Models.UserContext db = new Models.UserContext();
            db.Appoitments.Add(new Models.Appoitment { EmailUser = email, DateId = idDate, TimeId=idTime , IdDoctorSpecialty = appoitment.IdDoctorSpecialty });
            db.SaveChanges();
            db.Receptions.FirstOrDefault(x => x.Id == idTime).IsUse = true;
            db.SaveChanges();
            return RedirectToAction("Index", "User");

        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var email = HttpContext.User.Identity.Name;
                Models.UserContext db = new Models.UserContext();
                var idForDelete = Service.AppointmentForUser.GetList(email).FirstOrDefault(x => x.Id == id).IdAppoitment;
                var appointment = db.Appoitments.FirstOrDefault(x => x.Id == idForDelete);
                db.Receptions.FirstOrDefault(x => x.Id == appointment.TimeId).IsUse = false;
                db.Appoitments.Remove(appointment);
                db.SaveChanges();

                if (Request.IsAjaxRequest())
                {
                    return Json(new { personId = id });
                }

                return RedirectToAction("Index");
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
        public ActionResult ShowRecords()
        {
            var email= HttpContext.User.Identity.Name;
            Models.UserContext db = new Models.UserContext();
            return View(db.MedicalRecords.Where(x=>x.EmailCustomer==email));
        }
    }
}