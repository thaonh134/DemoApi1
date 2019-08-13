using DemoApi.Models.Account;
using DemoApi.Models.Users;
using DemoApi.Models.Users.UserManagerModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Services.Services.Interface
{
    public interface IUserService
    {
        UserMoreInfoModel GetUserInfor();

        Task<IdentityResult> EditUserInfor(EditUserInforModel model);

        Task<LoginResultModel> Login(LoginModel loginModel);
    }
}
