using AutoMapper;
using DemoApi.Common.Model;
using DemoApi.Database.DatabaseContext;
using DemoApi.Database.IdentityContext;
using DemoApi.Models.Users;
using DemoApi.Models.Users.UserManagerModel;
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
            CreateMap<ApplicationUser, EditUserInforModel>();
            CreateMap<AspNetUser, EditUserInforModel>();
            CreateMap<EditUserInforModel, ApplicationUser>()
               .ForMember(dst => dst.Email, opt => opt.Ignore())
               .ForMember(dst => dst.Id, opt => opt.Ignore())
               .ForMember(dst => dst.CreatedDate, opt => opt.Ignore())
               .ForMember(dst => dst.EmailConfirmed, opt => opt.Ignore())
               .ForMember(dst => dst.PasswordHash, opt => opt.Ignore())
               .ForMember(dst => dst.SecurityStamp, opt => opt.Ignore())
               .ForMember(dst => dst.PhoneNumberConfirmed, opt => opt.Ignore())
               .ForMember(dst => dst.TwoFactorEnabled, opt => opt.Ignore())
               .ForMember(dst => dst.LockoutEndDateUtc, opt => opt.Ignore())
               .ForMember(dst => dst.LockoutEnabled, opt => opt.Ignore())
               .ForMember(dst => dst.AccessFailedCount, opt => opt.Ignore())
               
            ;
        }
    }
}
