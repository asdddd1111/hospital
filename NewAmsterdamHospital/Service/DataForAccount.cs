using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace NewAmsterdamHospital.Service
{
    public class DataForAccount
    {
        public ViewModels.DataUser GetUser(string email)
        {
            ViewModels.DataUser user = new ViewModels.DataUser();
            Models.UserContext db = new Models.UserContext();
            foreach (var list in db.Users)
            {
                if (list.Email == email)
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<Models.User, ViewModels.DataUser>());
                    var mapper = new Mapper(config);
                    user = mapper.Map<Models.User, ViewModels.DataUser>(list);
                }
            }

            return user;
        }
    }
}