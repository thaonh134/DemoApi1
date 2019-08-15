using AutoMapper;
using DemoApi.Common.Model;
using DemoApi.Database.DatabaseContext;
using DemoApi.Database.IdentityContext;
using DemoApi.Models.Diarys;
using DemoApi.Models.Medias;
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
            CreateMap<AspNetUser, ViewUserModel>();
            CreateMap<AspNetUser, UserMoreInfoModel>().
                AfterMap((s, dst) =>
            {

                dst.AdmireCount = 0;
                dst.CommendCount = 0;
                dst.DiprieReadCount = 0;
                dst.FavoriteDiary = new List<ViewDiaryModel>();
                dst.FavoriteMedia = new List<ViewMediaModel>();

                var listFavoriteDiary= new List<ViewDiaryModel>();
                var listFavoriteMedia = new List<ViewMediaModel>();
                for (var i = 1; i <= 3; i++) {
                    listFavoriteDiary.Add(new ViewDiaryModel() { Id = i, UrlMedia = "default" });
                }
                for (var i = 1; i <= 3; i++)
                {
                    listFavoriteMedia.Add(new ViewMediaModel() { Id = i, Url = "default" });
                }
                dst.FavoriteDiary = listFavoriteDiary;
                dst.FavoriteMedia = listFavoriteMedia;
            });

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
