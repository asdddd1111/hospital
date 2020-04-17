using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace NewAmsterdamHospital.Service
{
    public class DataForEdit
    {
        public void Edit(string email, ViewModels.DataUser userEdit)
        {
            Models.UserContext db = new Models.UserContext();
            var user = db.Users.Where(c => c.Email == email).FirstOrDefault();
            user.Name = userEdit.Name;
            user.Surname = userEdit.Surname;
            user.DateOfBirth = userEdit.DateOfBirth;
            user.PhoneNumber = userEdit.PhoneNumber;
            db.SaveChanges();

        }
    }
}