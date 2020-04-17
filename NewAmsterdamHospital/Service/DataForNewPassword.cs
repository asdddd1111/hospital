using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewAmsterdamHospital.Service
{
    public class DataForNewPassword
    {
        public void Change(ViewModels.DataForChangePassword password, string email)
        {

            Models.UserContext db = new Models.UserContext();
            var user = db.Users
            .Where(c => c.Email == email)
            .FirstOrDefault();
            user.Password = password.NewPassword;
            db.SaveChanges();
        }
    }
}