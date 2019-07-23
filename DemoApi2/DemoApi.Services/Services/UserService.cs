using DemoApi.Common.Enums;
using DemoApi.Common.Exceptions;
using DemoApi.Common.Helper;
using DemoApi.Database.IdentityContext;
using DemoApi.Models.Account;
using DemoApi.Models.Users;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DemoApi.Services.Services.Interface;
using DemoApi.Services.Common;

namespace DemoApi.Services.Services
{
    public class UserService : BaseService, IUserService
    {
        
        public UserMoreInfoModel GetUserInfor()
        {
            var userId = CurrentUserIdentityClaimHelper.UserId;
            var user = UserMoreInfoModel.GetMoreUserInfor(userId);
            return user;
        }
    }
}
