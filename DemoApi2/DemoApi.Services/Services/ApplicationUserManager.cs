using DemoApi.Common.Enums;
using DemoApi.Common.Exceptions;
using DemoApi.Database.DatabaseContext;
using DemoApi.Database.IdentityContext;
using DemoApi.Database.Repositories;
using DemoApi.Database.Repositories.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Services
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = false
            };
            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }

        #region MyRegion
        public async Task BaseRegisterUser(ApplicationUser user)
        {
            IdentityResult result = await this.CreateAsync(user);
            if (!result.Succeeded)
            {
                throw new BaseApiException("RegisterUser_Fail", result.Errors.First());
            }

            result = await this.AddToRolesAsync(user.Id, RoleType.Member.DescriptionAttr());
            if (!result.Succeeded)
            {
                throw new BaseApiException("Assign_Role_To_User_Fail", result.Errors.First());
            }
            

        }
        #endregion
        #region Method with user claims
        public async Task EditUserClaims(string userId, string ClaimType, string ClaimValue)
        {
            var userClaims = await this.GetClaimsAsync(userId);
            var resultClaims = userClaims.Where(c => c.Type == ClaimType).ToList();
            if (resultClaims != null || resultClaims.Count > 0)
            {
                foreach (var item in resultClaims) await this.RemoveClaimAsync(userId, item);

            }
            await this.AddClaimAsync(userId, new Claim(ClaimType, ClaimValue));
        }

        public async Task<string> GetUserClaimValue(string userId, string ClaimType)
        {
            var userClaims = await this.GetClaimsAsync(userId);
            var resultClaims = userClaims.Where(c => c.Type == ClaimType).ToList();
            if (resultClaims != null && resultClaims.Count > 0)
            {
                return resultClaims[0].Value;
            }
            else return null;
        }

        


        //public async Task<string> GeneratOtpClaim(string userId, string ToNumber)
        //{
        //    Random rnd = new Random();
        //    var OtpValue = rnd.Next(100000, 999999).ToString();
        //    await EditUserClaims(userId, UserDefine.OtpValueClaimType, OtpValue);
        //    await EditUserClaims(userId, UserDefine.OtpExpiredClaimType, Convert.ToDouble(DateTime.UtcNow.AddMinutes(6).Ticks).ToString());
        //    await EditUserClaims(userId, UserDefine.OtpActiveClaimType, ((int)ActiveStatus.Active).ToString());
        //    await EditUserClaims(userId, UserDefine.OtpToNumberClaimType, ToNumber);
        //    return OtpValue;
        //}
        //public async Task ValidateOtpClaimResult(string userId, string ToNumber, string OtpInput)
        //{
        //    //get Otp just send
        //    var OtpValue = await GetUserClaimValue(userId, UserDefine.OtpValueClaimType);
        //    var OtpExpired = await GetUserClaimValue(userId, UserDefine.OtpExpiredClaimType);
        //    var OtpActive = await GetUserClaimValue(userId, UserDefine.OtpActiveClaimType);
        //    var OtpToNumber = await GetUserClaimValue(userId, UserDefine.OtpToNumberClaimType);

        //    //valid OtpValidFail
        //    if (string.IsNullOrEmpty(OtpValue) || string.IsNullOrEmpty(OtpExpired))
        //        throw new VOVBaseApiException(nameof(UserResourceMessage.Verify_UserLogin_OtpValidFail), UserResourceMessage.Verify_UserLogin_OtpValidFail);
        //    //valid OtpNotActive
        //    else if (OtpActive != ((int)ActiveStatus.Active).ToString())
        //        throw new VOVBaseApiException(nameof(UserResourceMessage.Verify_UserLogin_OtpNotActive), UserResourceMessage.Verify_UserLogin_OtpNotActive);

        //    //valid OtpExpired
        //    else if (Convert.ToDouble(OtpExpired) < Convert.ToDouble(DateTime.UtcNow.Ticks))
        //        throw new VOVBaseApiException(nameof(UserResourceMessage.Verify_UserLogin_OtpExpired), UserResourceMessage.Verify_UserLogin_OtpExpired);

        //    //valid OtpNotMatch
        //    else if (OtpValue != OtpInput || OtpToNumber != ToNumber)
        //        throw new VOVBaseApiException(nameof(UserResourceMessage.Verify_UserLogin_OtpNotMatch), UserResourceMessage.Verify_UserLogin_OtpNotMatch);

        //}

        #endregion

        #region Check Block User
        //public void BaseCheckUserLogin(ApplicationUser user)
        //{
        //    if (
        //        ((user.BlockType == (int)BlockType.Day1 || user.BlockType == (int)BlockType.Day7) && user.BlockExpired.Value > DateTime.Now)
        //        ||
        //        (user.BlockType == (int)BlockType.DayForever)
        //        )
        //    {
        //        throw new VOVBaseApiException(nameof(UserResourceMessage.Verify_UserLogin_UserIsBlock), UserResourceMessage.Verify_UserLogin_UserIsBlock);
        //    }
        //}
        #endregion

    }
}
