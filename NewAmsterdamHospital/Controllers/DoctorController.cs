using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewAmsterdamHospital.Controllers
{ 
    [Authorize(Roles ="doctor")]
    public class DoctorController : Controller
    {
        // GET: Doctor
        public ActionResult Index()
        {
            var email = HttpContext.User.Identity.Name;
            List<ViewModels.ListCustomer> listCustomer = new List<ViewModels.ListCustomer>();
            Models.UserContext db = new Models.UserContext();
            var doctorId=db.Users.FirstOrDefault(x => x.Email == email).DoctorSpecialtyId;
            foreach (var list in db.Appoitments.Where(x => x.IdDoctorSpecialty == doctorId))
            {
                listCustomer.Add(new ViewModels.ListCustomer {EmailCustomer=list.EmailUser, Name = db.Users.FirstOrDefault(x => x.Email == list.EmailUser).Name, Surname = db.Users.FirstOrDefault(x => x.Email == list.EmailUser).Surname, Date = db.DateReceptions.FirstOrDefault(x => x.Id == list.DateId).DateName, Time = db.Receptions.FirstOrDefault(x => x.Id == list.TimeId).TimeName });
            }
            return View(listCustomer);
        }

        public ActionResult ShowSchedule()
        {
            List<ViewModels.Schedule> schedules = new List<ViewModels.Schedule>();
            Models.UserContext db = new Models.UserContext();
            var email = HttpContext.User.Identity.Name;
            var doctorId=db.Users.FirstOrDefault(x => x.Email == email).DoctorSpecialtyId;
            List<int> idDate = new List<int>();
            foreach (var list in db.DateReceptions.Where(x=>x.DoctorSpecialtyId==doctorId))
            {
                idDate.Add(list.Id);
            }
            foreach (var id in idDate)
            {
                foreach (var list in db.DateReceptions.Where(x => x.Id == id&&x.Date>DateTime.Now))
                {
                    foreach (var listTime in db.Receptions.Where(x => x.DateReceptionId == list.Id&&x.IsUse==false))
                    {
                        schedules.Add(new ViewModels.Schedule { Id=listTime.Id, DateName =list.DateName,TimeName=listTime.TimeName});
                        
                    }
                }
            }
            if (Request.IsAjaxRequest())
            {
                return PartialView("ShowSchedule", schedules);
            }

            return View(schedules);
        }
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Person/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var email = HttpContext.User.Identity.Name;
                Models.UserContext db = new Models.UserContext();
                var schedule = db.Receptions.FirstOrDefault(x => x.Id == id);
                db.Receptions.Remove(schedule);
                db.SaveChanges();

                // TODO: Add delete logic here
                if (Request.IsAjaxRequest())
                {
                    return Json(new { scheduleId = id });
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
       
        public ActionResult Record(string email)
        {
            Models.UserContext db = new Models.UserContext();
            var medicalOfRecord = db.MedicalRecords.Where(x => x.EmailCustomer == email);
            return View(medicalOfRecord);
        }
        [HttpPost]
        public ActionResult Record(string email, string text)
        {
            text = Request.Form["MedicalRecord"];
            var emailDoctor = HttpContext.User.Identity.Name;
            Models.UserContext db = new Models.UserContext();
            db.MedicalRecords.Add(new Models.MedicalRecord {EmailCustomer=email, DateOfChange=DateTime.Now, DoctorSpecialty=db.DoctorSpecialties.FirstOrDefault(y=>y.Id==db.Users.FirstOrDefault(x=>x.Email==emailDoctor).DoctorSpecialtyId).Name, Record=text });
            db.SaveChanges();
            return RedirectToAction("Index", "Doctor");
        }
       
    }
}