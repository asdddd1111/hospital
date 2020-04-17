using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewAmsterdamHospital.Service
{
    public static class ListUsersCustomer
    {
        public static List<ViewModels.DataCustomerForAdmin> GetList()
        {
            Models.UserContext db = new Models.UserContext();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Models.User, ViewModels.DataCustomerForAdmin>());
            var mapper = new Mapper(config);
            var datas = mapper.Map<List<ViewModels.DataCustomerForAdmin>>(db.Users.Where(x=>x.RoleId==2));
            return datas;
        }
    }
    public static class ListUsersDoctor
    {
        public static List<ViewModels.DataDoctorForAdmin> GetList()
        {
            Models.UserContext db = new Models.UserContext();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Models.User, ViewModels.DataDoctorForAdmin>());
            var mapper = new Mapper(config);
            var datas = mapper.Map<List<ViewModels.DataDoctorForAdmin>>(db.Users.Where(x => x.RoleId == 3));
            foreach (var list in datas)
            {
                list.DoctorSpecialty = db.DoctorSpecialties.FirstOrDefault(x => x.Id == db.Users.FirstOrDefault(y => y.Email == list.Email).DoctorSpecialtyId).Name;
                list.NumberRoom =db.NumberRooms.FirstOrDefault(z=>z.Id== db.DoctorSpecialties.FirstOrDefault(x => x.Id == db.Users.FirstOrDefault(y => y.Email == list.Email).DoctorSpecialtyId).NumberRoomId).Number;
            }
            return datas;
        }
    }
}