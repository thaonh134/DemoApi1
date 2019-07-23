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

namespace DemoApi.Controllers.V1
{


    [RoutePrefix("api/v1/users")]
    public class UsersController: DemoApiBaseController
    {
        private IUserService _userService;
        private IAspNetUserRepository _aspNetUserRepository;
       
        public UsersController(IUserService userService, IAspNetUserRepository aspNetUserRepository)
        {
            _userService = userService;
            _aspNetUserRepository = aspNetUserRepository;
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
    }
}