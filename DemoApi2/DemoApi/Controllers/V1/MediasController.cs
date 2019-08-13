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
using DemoApi.Common.Pagination;
using DemoApi.Common.Extension;
using System.Threading.Tasks;
using DemoApi.APIFilter;

namespace DemoApi.Controllers.V1
{
    [RoutePrefix("api/v1/medias")]
    [DemoAuthorizeAttribute]
    public class MediasController : DemoApiBaseController
    {
        private IMediaService _mediaService;
        private IAspNetUserRepository _aspNetUserRepository;
        private IUserService _userService;

        public MediasController(IMediaService mediaService, IAspNetUserRepository aspNetUserRepository,
            IUserService userService)
        {
            _aspNetUserRepository = aspNetUserRepository;
            _mediaService = mediaService;
            _userService = userService;
        }

        #region Media

        [Route("getAllMedias")]
        [HttpGet]
        public IHttpActionResult GetAllMedias(HttpRequestMessage requestMessage)
        {
            var users = _userService.GetUserInfor();
            if (users == null)
                return new TCErrorHttpActionResult(requestMessage, "AUTH_0001", "Phiên truy cập không được phép()");
            var medias = _mediaService.GetAll();
            if (medias == null)
                return new TCErrorHttpActionResult(requestMessage, "NOT FOUND", "Không có dữ liệu");

            var dataResponse = medias;

            return new TCSuccessHttpActionResult(requestMessage, dataResponse);
        }
        [Route("getMediaByUserId")]
        [HttpGet]
        public async Task<IHttpActionResult> GetMediaByUserId(HttpRequestMessage requestMessage
            , string userId
            , int? pageNumber = null,
            int? pageSize = null)
        {
            PaginationRequest pagedDataRequest = new PaginationRequest(pageNumber.DefaultZeroIfNull(), pageSize);
            return new TCSuccessHttpActionResult(requestMessage, await _mediaService.GetMediaByUserId(pagedDataRequest, userId));
        }
        [Route("CreateMedia")]
        [HttpPost]
        public IHttpActionResult CreateMedia(HttpRequestMessage requestMessage, AddMediaModel model)
        {
            var users = _userService.GetUserInfor();
            if (users == null)
                return new TCErrorHttpActionResult(requestMessage, "AUTH_0001", "Phiên truy cập không được phép()");
            var medias = _mediaService.Create(model);
            if (medias == 0)
                return new TCErrorHttpActionResult(requestMessage, "ERROR", "Có lỗi xảy ra");

            var dataResponse = medias;

            return new TCSuccessHttpActionResult(requestMessage, dataResponse);
        }
        [Route("UpdateMedia")]
        [HttpPost]
        public IHttpActionResult UpdateMedia(HttpRequestMessage requestMessage, EditMediaModel model)
        {
            var users = _userService.GetUserInfor();
            if (users == null)
                return new TCErrorHttpActionResult(requestMessage, "AUTH_0001", "Phiên truy cập không được phép()");
            var medias = _mediaService.Update(model);
            if (medias == 0)
                return new TCErrorHttpActionResult(requestMessage, "ERROR", "Có lỗi xảy ra");

            var dataResponse = medias;

            return new TCSuccessHttpActionResult(requestMessage, dataResponse);
        }
        [Route("DeleteMedia")]
        [HttpPost]
        public IHttpActionResult DeleteMedia(HttpRequestMessage requestMessage, int id)
        {
            var users = _userService.GetUserInfor();
            if (users == null)
                return new TCErrorHttpActionResult(requestMessage, "AUTH_0001", "Phiên truy cập không được phép()");
            var medias = _mediaService.Delete(id);
            if (medias == 0)
                return new TCErrorHttpActionResult(requestMessage, "ERROR", "Có lỗi xảy ra");

            var dataResponse = medias;

            return new TCSuccessHttpActionResult(requestMessage, dataResponse);
        }

        #endregion

        #region MediaFavorite
        [Route("GetAllFavoriteByMediaId")]
        [HttpGet]
        public IHttpActionResult GetAllFavoriteByMediaId(HttpRequestMessage requestMessage, int mediaId)
        {
            var users = _userService.GetUserInfor();
            if (users == null)
                return new TCErrorHttpActionResult(requestMessage, "AUTH_0001", "Phiên truy cập không được phép()");
            var medias = _mediaService.GetAllFavoriteByMediaId(mediaId);
            if (medias == null)
                return new TCErrorHttpActionResult(requestMessage, "NOT FOUND", "Không có dữ liệu");

            var dataResponse = medias;

            return new TCSuccessHttpActionResult(requestMessage, dataResponse);
        }
        [Route("AddFavorite")]
        [HttpPost]
        public IHttpActionResult AddFavorite(HttpRequestMessage requestMessage, int mediaId)
        {
            var users = _userService.GetUserInfor();
            if (users == null)
                return new TCErrorHttpActionResult(requestMessage, "AUTH_0001", "Phiên truy cập không được phép()");
            var medias = _mediaService.AddFavorite(mediaId);
            if (medias == 0)
                return new TCErrorHttpActionResult(requestMessage, "ERROR", "Có lỗi xảy ra");

            var dataResponse = medias;

            return new TCSuccessHttpActionResult(requestMessage, dataResponse);
        }
        [Route("DeleteFavorite")]
        [HttpPost]
        public IHttpActionResult DeleteFavorite(HttpRequestMessage requestMessage, int id)
        {
            var users = _userService.GetUserInfor();
            if (users == null)
                return new TCErrorHttpActionResult(requestMessage, "AUTH_0001", "Phiên truy cập không được phép()");
            var medias = _mediaService.AddFavorite(id);
            if (medias == 0)
                return new TCErrorHttpActionResult(requestMessage, "ERROR", "Có lỗi xảy ra");

            var dataResponse = medias;

            return new TCSuccessHttpActionResult(requestMessage, dataResponse);
        }
        #endregion

        #region MediaComment
        [Route("GetAllCommentByMediaId")]
        [HttpGet]
        public IHttpActionResult GetAllCommentByMediaId(HttpRequestMessage requestMessage, int mediaId)
        {
            var users = _userService.GetUserInfor();
            if (users == null)
                return new TCErrorHttpActionResult(requestMessage, "AUTH_0001", "Phiên truy cập không được phép()");
            var medias = _mediaService.GetAllCommentByMediaId(mediaId);
            if (medias == null)
                return new TCErrorHttpActionResult(requestMessage, "NOT FOUND", "Không có dữ liệu");

            var dataResponse = medias;

            return new TCSuccessHttpActionResult(requestMessage, dataResponse);
        }
        [Route("AddComment")]
        [HttpPost]
        public IHttpActionResult AddComment(HttpRequestMessage requestMessage, MediaCommentModel model)
        {
            var users = _userService.GetUserInfor();
            if (users == null)
                return new TCErrorHttpActionResult(requestMessage, "AUTH_0001", "Phiên truy cập không được phép()");
            var medias = _mediaService.AddComment(model);
            if (medias == 0)
                return new TCErrorHttpActionResult(requestMessage, "ERROR", "Có lỗi xảy ra");

            var dataResponse = medias;

            return new TCSuccessHttpActionResult(requestMessage, dataResponse);
        }
        [Route("RepComment")]
        [HttpPost]
        public IHttpActionResult RepComment(HttpRequestMessage requestMessage, MediaCommentModel model)
        {
            var users = _userService.GetUserInfor();
            if (users == null)
                return new TCErrorHttpActionResult(requestMessage, "AUTH_0001", "Phiên truy cập không được phép()");
            var medias = _mediaService.RepComment(model);
            if (medias == 0)
                return new TCErrorHttpActionResult(requestMessage, "ERROR", "Có lỗi xảy ra");

            var dataResponse = medias;

            return new TCSuccessHttpActionResult(requestMessage, dataResponse);
        }

        #endregion

        #region MediaDetail

        [Route("AddMediaDetail")]
        [HttpPost]
        public IHttpActionResult AddMediaDetail(HttpRequestMessage requestMessage, MediaDetailModel model)
        {
            var users = _userService.GetUserInfor();
            if (users == null)
                return new TCErrorHttpActionResult(requestMessage, "AUTH_0001", "Phiên truy cập không được phép()");
            var medias = _mediaService.AddMediaDetail(model);
            if (medias == 0)
                return new TCErrorHttpActionResult(requestMessage, "ERROR", "Có lỗi xảy ra");

            var dataResponse = medias;

            return new TCSuccessHttpActionResult(requestMessage, dataResponse);
        }
        [Route("DeleteMediaDetail")]
        [HttpPost]
        public IHttpActionResult DeleteMediaDetail(HttpRequestMessage requestMessage, int id)
        {
            var users = _userService.GetUserInfor();
            if (users == null)
                return new TCErrorHttpActionResult(requestMessage, "AUTH_0001", "Phiên truy cập không được phép()");
            var medias = _mediaService.DeleteMediaDetail(id);
            if (medias == 0)
                return new TCErrorHttpActionResult(requestMessage, "ERROR", "Có lỗi xảy ra");

            var dataResponse = medias;

            return new TCSuccessHttpActionResult(requestMessage, dataResponse);
        }
        #endregion
    }
}