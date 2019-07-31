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
    public class MediaFavoriteRepository : BaseRepository<MediaFavorite, test15_api_everEntities>, IMediaFavoriteRepository
    {
        public MediaFavoriteRepository(test15_api_everEntities dbContext) : base(dbContext)
        {
            
        }
    }
}
