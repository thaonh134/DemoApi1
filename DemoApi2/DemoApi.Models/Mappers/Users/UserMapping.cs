using AutoMapper;
using DemoApi.Common.Model;
using DemoApi.Database.DatabaseContext;
using DemoApi.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.AutoMapConfig.UserMap
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<AspNetUser, UserMoreInfoModel>();

            CreateMap<AspNetUser, AspNetUserCommon>();
        }
    }
}
