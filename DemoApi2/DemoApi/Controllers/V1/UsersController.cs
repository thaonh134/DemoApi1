using DemoApi.Database.DatabaseContext;
using DemoApi.Database.Repositories.Interfaces;
using DemoApi.Services.Services.Interface;
using DemoApi.HttpActionResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Threading.Tasks;
using DemoApi.Models.Users.UserManagerModel;
using DemoApi.Models;
using DemoApi.Database.IdentityContext;
using DemoApi.Services;
using Microsoft.AspNet.Identity;

namespace DemoApi.Controllers.V1
{


    [RoutePrefix("api/v1/users")]
    public class UsersController: DemoApiBaseController
    {
        private IUserService _userService;
        private IAspNetUserRepository _aspNetUserRepository;
        private ApplicationUserManager _userManager;

        public UsersController(IUserService userService, IAspNetUserRepository aspNetUserRepository, ApplicationUserManager userManager
           )
        {
            _userService = userService;
            _aspNetUserRepository = aspNetUserRepository;
            _userManager = userManager;
        }
        [Route("getUserInfor")]
        [HttpGet]
        public IHttpActionResult GetUserInfor(HttpRequestMessage requestMessage)
        {
            var users = _userService.GetUserInfor();
            if (users == null)
                return new TCErrorHttpActionResult(requestMessage, "AUTH_0001", "Phiên truy cập không được phép()");

            var dataResponse = users;

            return new TCSuccessHttpActionResult(requestMessage, dataResponse);
        }
        [Route("register")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> RegisterUser(HttpRequestMessage requestMessage, RegisterBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser() { UserName = model.Email, Email = model.Email, CreatedDate = DateTime.UtcNow };

            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return new TCSuccessHttpActionResult(requestMessage, true);
        }

        [Route("edituserinfor")]
        [HttpPost]
        public async Task<IHttpActionResult> EditUserInfor(HttpRequestMessage requestMessage, EditUserInforModel model)
        {
            var result = await _userService.EditUserInfor(model);
            return new TCSuccessHttpActionResult(requestMessage, result.Succeeded);
        }
        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}