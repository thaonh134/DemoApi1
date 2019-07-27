using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoApi.Database.Base;
using DemoApi.Database.DatabaseContext;
using DemoApi.Database.Repositories.Interfaces;

namespace DemoApi.Database.Repositories
{
    public class MediaCommentDetailRepository : BaseRepository<MediaCommentDetail, test15_api_everEntities>, IMediaCommentDetailRepository
    {
        public MediaCommentDetailRepository(test15_api_everEntities dbContext) : base(dbContext)
        {
            
        }
    }
}
