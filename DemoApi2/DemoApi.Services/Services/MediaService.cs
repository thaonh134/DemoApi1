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
using AutoMapper;
using DemoApi.Database.Base;
using DemoApi.Database.DatabaseContext;
using DemoApi.Database.Repositories.Interfaces;
using DemoApi.Models.Medias;
using DemoApi.Services.Services.Interface;
using DemoApi.Services.Common;
using DemoApi.Common.Pagination;

namespace DemoApi.Services.Services
{
    public class MediaService : BaseService, IMediaService
    {
        private IUnitOfWork _unitOfWork;
        private IMediaRepository _mediaRepository;
        private IMediaFavoriteRepository _mediaFavoriteRepository;
        private IMediaCommentRepository _mediaCommentRepository;
        private IMediaCommentDetailRepository _mediaCommentDetailRepository;
        private IMediaDetailRepository _mediaDetailRepository;
        public MediaService(IUnitOfWork unitOfWork,
            IMediaRepository mediaRepository,
            IMediaFavoriteRepository mediaFavoriteRepository,
            IMediaCommentRepository mediaCommentRepository,
            IMediaCommentDetailRepository mediaCommentDetailRepository,
            IMediaDetailRepository mediaDetailRepository)
        {
            _unitOfWork = unitOfWork;
            _mediaRepository = mediaRepository;
            _mediaFavoriteRepository = mediaFavoriteRepository;
            _mediaCommentRepository = mediaCommentRepository;
            _mediaCommentDetailRepository = mediaCommentDetailRepository;
            _mediaDetailRepository = mediaDetailRepository;
        }

        #region Media

        public int Create(AddMediaModel model)
        {
            model.UserId = CurrentUserIdentityClaimHelper.UserId;
            var itemEntry = Mapper.Map<Medium>(model);
            _mediaRepository.Create(itemEntry);
            return _unitOfWork.SaveChanges();
        }
        public int Update(EditMediaModel model)
        {
            model.UserId = CurrentUserIdentityClaimHelper.UserId;
            var itemEntry = Mapper.Map<Medium>(model);
            _mediaRepository.Update(itemEntry);
            return _unitOfWork.SaveChanges();
        }
        public int Delete(int id)
        {
            var itemEntry = _mediaRepository.Get(x => x.Id == id);
            _mediaRepository.Delete(itemEntry);
            return _unitOfWork.SaveChanges();
        }
        public List<MediaModel> GetAll()
        {
            var items = _mediaRepository.GetAll();
            if (items == null) return new List<MediaModel>();
            return Mapper.Map<List<MediaModel>>(items);
        }
        public async Task<PaginationAndDataResult<MediaModel>> GetMediaByUserId(PaginationRequest pageDataRequest, string UserId)
        {
            int? lastkey = null;
            if (!string.IsNullOrEmpty(pageDataRequest.LastKey))
            {
                int id;
                int.TryParse(pageDataRequest.LastKey, out id);
                lastkey = id;
            }
            var items = await _mediaRepository.SelectPageAsync(pageDataRequest, _mediaRepository.ExpressionSearch(UserId, lastkey), x => x.OrderByDescending(t => t.Id));
            return Mapper.Map<PaginationAndDataResult<MediaModel>>(items);
        }

        #endregion

        #region MediaFavorite
        public List<MediaCommentModel> GetAllFavoriteByMediaId(int mediaId)
        {
            var items = _mediaFavoriteRepository.GetAll().Where(x => x.MediaId.HasValue && x.MediaId.Value == mediaId).ToList();
            if (items == null) return new List<MediaCommentModel>();
            return Mapper.Map<List<MediaCommentModel>>(items);
        }
        public int AddFavorite(int mediaId)
        {
            var userId = CurrentUserIdentityClaimHelper.UserId;
            MediaFavorite itemEntry = new MediaFavorite()
            {
                UserId = userId,
                MediaId = mediaId
            };
            _mediaFavoriteRepository.Create(itemEntry);
            return _unitOfWork.SaveChanges(); ;
        }
        public int DeleteFavorite(int id)
        {
            var itemEntry = _mediaFavoriteRepository.Get(x => x.Id == id);

            _mediaFavoriteRepository.Delete(itemEntry);
            return _unitOfWork.SaveChanges(); ;
        }

        #endregion

        #region MediaComment

        public List<MediaCommentModel> GetAllCommentByMediaId(int mediaId)
        {
            var items = _mediaCommentRepository.GetAll().Where(x => x.MediaId.HasValue && x.MediaId.Value == mediaId).ToList();
            if (items == null) return new List<MediaCommentModel>();
            return Mapper.Map<List<MediaCommentModel>>(items);
        }
        public int AddComment(MediaCommentModel model)
        {
            var userId = CurrentUserIdentityClaimHelper.UserId;
            model.ByUserId = userId;
            var itemEntry = Mapper.Map<MediaComment>(model);
            _mediaCommentRepository.Create(itemEntry);
            return _unitOfWork.SaveChanges(); ;
        }
        public int RepComment(MediaCommentModel model)
        {
            var userId = CurrentUserIdentityClaimHelper.UserId;
            model.ByUserId = userId;
            var itemEntry = Mapper.Map<MediaCommentDetail>(model);
            _mediaCommentDetailRepository.Create(itemEntry);
            return _unitOfWork.SaveChanges(); ;
        }

        #endregion

        #region MediaDetail

        public int AddMediaDetail(MediaDetailModel model)
        {
            var userId = CurrentUserIdentityClaimHelper.UserId;
            model.ByUserId = userId;
            var itemEntry = Mapper.Map<MediaDetai>(model);
            _mediaDetailRepository.Create(itemEntry);
            return _unitOfWork.SaveChanges(); ;
        }
        public int DeleteMediaDetail(int id)
        {
            var itemEntry = _mediaDetailRepository.Get(x => x.Id == id);

            _mediaDetailRepository.Delete(itemEntry);
            return _unitOfWork.SaveChanges(); ;
        }

        #endregion
    }
}
