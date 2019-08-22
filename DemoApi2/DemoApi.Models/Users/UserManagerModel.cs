using AutoMapper;
using DemoApi.Common.Enums;
using DemoApi.Database.DatabaseContext;
using DemoApi.Database.Repositories;
using DemoApi.Database.Repositories.Interfaces;
using DemoApi.Models.Diarys;
using DemoApi.Models.Medias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Models.Users
{
    class UserManagerModel
    {
    }
    public class ViewUserModel
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
        public Nullable<int> Gender { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }

    }

    public class RegisterUserModel
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        //public string Password { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Address { get; set; }
        public string ResourceUrl { get; set; }
        public List<int> AreaIds { get; set; }
        public List<int> AuthRoleIds { get; set; }

    }
    public class EditUserInforModel
    {
        //public string Id { get; set; }
        //public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Address { get; set; }
        public string ResourceUrl { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public Nullable<int> Gender { get; set; }
    }

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
        public int AdmireCount { get; set; }
        public int CommendCount { get; set; }
        public int DiprieReadCount { get; set; }
        public Nullable<int> Gender { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public List<ViewDiaryModel> FavoriteDiary { get; set; }
        public List<ViewMediaModel> FavoriteMedia { get; set; }

        public Nullable<System.DateTime> CreatedDate { get; set; }
        public static UserMoreInfoModel GetMoreUserInfor(string userId)
        {
            AspNetUser data = null;
            var result = new UserMoreInfoModel();
            using (test15_api_everEntities dbContext = new test15_api_everEntities())
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
            using (test15_api_everEntities dbContext = new test15_api_everEntities())
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
