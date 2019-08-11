using DemoApi.Models.Account;
using DemoApi.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoApi.Models.Medias;
using DemoApi.Common.Pagination;

namespace DemoApi.Services.Services.Interface
{
    public interface IMediaService
    {
        int Create(AddMediaModel model);
        int Update(EditMediaModel model);
        int Delete(int id);
        List<MediaModel> GetAll();
        Task<PaginationAndDataResult<MediaModel>>  GetMediaByUserId(PaginationRequest pageDataRequest, string UserId);
        List<MediaCommentModel> GetAllFavoriteByMediaId(int mediaId);
        int AddFavorite(int mediaId);
        int DeleteFavorite(int id);
        List<MediaCommentModel> GetAllCommentByMediaId(int mediaId);
        int AddComment(MediaCommentModel model);
        int RepComment(MediaCommentModel model);
        int AddMediaDetail(MediaDetailModel model);
        int DeleteMediaDetail(int id);

    }
}
