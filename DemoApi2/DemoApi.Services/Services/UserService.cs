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
using DemoApi.Models.Users.UserManagerModel;
using Microsoft.AspNet.Identity;
using DemoApi.Database.DatabaseContext;
using DemoApi.Database.Repositories.Interfaces;
using AutoMapper;

namespace DemoApi.Services.Services
{
    public class UserService : BaseService, IUserService
    {

        private ApplicationUserManager _userManager;
        private IAspNetUserRepository _aspNetUserRepository;
        public UserService(

            ApplicationUserManager userManager,
            IAspNetUserRepository aspNetUserRepository)
        {
            _aspNetUserRepository = aspNetUserRepository;
            _userManager = userManager;
        }
        public async Task<IdentityResult> EditUserInfor(EditUserInforModel model)
        {
            //var userResponsitory = await GetById(model.Id);
            //if (userResponsitory == null) throw new BaseApiException("NotExist", "NotExist");

            var userId = CurrentUserIdentityClaimHelper.UserId;

            var user = await _userManager.FindByIdAsync(userId);
            
            var resultTask = new IdentityResult();
            try
            {
                #region valid data

                #endregion
                #region UserData

                #region update Url
                if (!string.IsNullOrEmpty(model.ResourceUrl))
                {
                    var mulResourceUrl = model.ResourceUrl.Split(';');
                    var lstResultResource = new List<string>();
                    foreach (var item in mulResourceUrl)
                    {
                        //move file from temp folder to resource folder
                        //string resultUrl = await _fileManager.MoveFileAsyn(item, _fileManager.ReplacePath(item, _appSettingWebConfig.rootTempPath, _appSettingWebConfig.rootResourcePath));
                        lstResultResource.Add(item);
                    }
                    model.ResourceUrl = String.Join(";", lstResultResource.ToArray());
                }

                #endregion
                user = Mapper.Map(model, user);
                // Then process:
                resultTask = await _userManager.UpdateAsync(user);

                //
                if (!resultTask.Succeeded)
                {
                    throw new BaseApiException("UpdateUser_Fail", resultTask.Errors.First());
                }
                #endregion
                
            }
            catch (Exception ex)
            {
                return new IdentityResult();
            }

            return resultTask;
        }

        public UserMoreInfoModel GetUserInfor()
        {
            var userId = CurrentUserIdentityClaimHelper.UserId;
            var user = UserMoreInfoModel.GetMoreUserInfor(userId);
            return user;
        }
        private async Task<AspNetUser> GetById(string userId)
        {
            var user = await _aspNetUserRepository.GetAsync(x => x.Id == userId);
            return user;
        }
    }
}
