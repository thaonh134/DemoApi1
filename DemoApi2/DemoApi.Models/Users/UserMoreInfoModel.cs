using AutoMapper;
using DemoApi.Common.Enums;
using DemoApi.Database.DatabaseContext;
using DemoApi.Database.Repositories;
using DemoApi.Database.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Models.Users
{
    public class UserMoreInfoModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string ResourceUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public static UserMoreInfoModel GetMoreUserInfor(string userId)
        {
            AspNetUser data = null;
            var result = new UserMoreInfoModel();
            using (EverliveEntities dbContext = new EverliveEntities())
            {
                IAspNetUserRepository aspNetUserRepository = new AspNetUserRepository(dbContext);
                data = aspNetUserRepository.Get(x => x.Id == userId);
                result = Mapper.Map<UserMoreInfoModel>(data);
            }
            return result;
        }
        public static List<UserMoreInfoModel> GetMoreUserInforByEmail(string Email, LoginType loginType)
        {
            var result = new List<UserMoreInfoModel>();
            using (EverliveEntities dbContext = new EverliveEntities())
            {
                IAspNetUserRepository aspNetUserRepository = new AspNetUserRepository(dbContext);
                var data = aspNetUserRepository.GetMulti(x => x.Email == Email);
                if (data == null) return null;
                 result = Mapper.Map<List<UserMoreInfoModel>>(data);
                
            }

            return result;
        }
    }
}
