using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using NewAmsterdamHospital.ViewModels;
using NewAmsterdamHospital.Models;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Data.Entity;

namespace NewAmsterdamHospital.Controllers
{
    
    public class AccountController : Controller
    {

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(SiginIn login)
        {
            if (ModelState.IsValid)
            {
                Models.User user = null;
                using (Models.UserContext db = new Models.UserContext())
                {
                    user = db.Users.FirstOrDefault(u => u.Email == login.Email && u.Password == login.Password);

                }
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(login.Email, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "There is no user with this username and password");
                }
            }

            return View(login);
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Register newUser)
        {
            if (ModelState.IsValid)
            {
                Models.User user = null;
                using (Models.UserContext db = new Models.UserContext())
                {
                    user = db.Users.FirstOrDefault(u => u.Email == newUser.Email);
                }
                if (user == null)
                {
                    using (Models.UserContext db = new Models.UserContext())
                    {
                        db.Users.Add(new Models.User { Name = newUser.Name, Surname = newUser.Surname, Password = newUser.Password, Email = newUser.Email, DateOfBirth = newUser.DateOfBirth, PhoneNumber = newUser.PhoneNumber, RoleId = 2 });
                        db.SaveChanges();

                        user = db.Users.Where(u => u.Email == newUser.Email && u.Password == newUser.Password).FirstOrDefault();
                    }
                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(newUser.Email, true);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "A user with this login already exists");
                }
            }

            return View(newUser);
        }
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }


    }
}
