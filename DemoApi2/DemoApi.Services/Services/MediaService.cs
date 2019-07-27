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

namespace DemoApi.Services.Services
{
    public class MediaService : BaseService, IMediaService
    {
        private IUnitOfWork _unitOfWork;
        private IMediaRepository _mediaRepository;
        public MediaService(IUnitOfWork unitOfWork,
            IMediaRepository mediaRepository)
        {
            _unitOfWork = unitOfWork;
            _mediaRepository = mediaRepository;
        }
        public int Create(AddMediaModel model)
        {
            var itemEntry = Mapper.Map<Medium>(model);
            _mediaRepository.Create(itemEntry);
            return _unitOfWork.SaveChanges();
        }
        public int Update(EditMediaModel model)
        {
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
    }
}
