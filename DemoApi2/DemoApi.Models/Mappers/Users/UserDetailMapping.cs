using AutoMapper;
using DemoApi.Database.DatabaseContext;
using DemoApi.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Models.Mappers.Users
{
    public class UserDetailMapping : Profile
    {
        public UserDetailMapping()
        {
            CreateMap<AspNetUser, UserMoreInfoModel>();

        }
    }
}
