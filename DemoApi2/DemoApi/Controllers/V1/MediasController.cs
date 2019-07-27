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
using DemoApi.Models.Medias;

namespace DemoApi.Controllers.V1
{
    [RoutePrefix("api/v1/medias")]
    public class MediasController : DemoApiBaseController
    {
        private IUserService _userService;
        private IMediaService _mediaService;
        
        private IAspNetUserRepository _aspNetUserRepository;

        public MediasController(IUserService userService, IMediaService mediaService, IAspNetUserRepository aspNetUserRepository)
        {
            _userService = userService;
            _mediaService = mediaService;
            _aspNetUserRepository = aspNetUserRepository;
        }
        [Route("getAllMedias")]
        [HttpGet]
        public IHttpActionResult GetAllMedias(HttpRequestMessage requestMessage)
        {
            var users = _userService.GetUserInfor();
            if (users == null)
                return new TCErrorHttpActionResult(requestMessage, "AUTH_0001", "Phiên truy cập không được phép()");

            var dataResponse = users;

            return new TCSuccessHttpActionResult(requestMessage, dataResponse);
        }
        [Route("add")]
        [HttpPost]
        public IHttpActionResult Add(HttpRequestMessage requestMessage, AddMediaModel model)
        {
            var users = _userService.GetUserInfor();
            if (users == null)
                return new TCErrorHttpActionResult(requestMessage, "AUTH_0001", "Phiên truy cập không được phép()");

            var dataResponse = users;

            return new TCSuccessHttpActionResult(requestMessage, dataResponse);
        }
    }
}