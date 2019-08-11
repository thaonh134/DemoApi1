using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DemoApi.Database.Base;
using DemoApi.Database.DatabaseContext;
using DemoApi.Database.Repositories.Interfaces;

namespace DemoApi.Database.Repositories
{
    public class MediaRepository : BaseRepository<Medium, test15_api_everEntities>, IMediaRepository
    {
        public MediaRepository(test15_api_everEntities dbContext) : base(dbContext)
        {
            
        }

        public Expression<Func<Medium, bool>> ExpressionSearch(string UserId, int? lastId)
        {
            //Expression<Func<Medium, bool>> ex_search;
            //if (lastId.HasValue)         ex_search = (x => x.UserId.Equals(UserId) && x.Id < lastId);
            //else ex_search = (x => x.UserId.Equals(UserId) );
            Expression<Func<Medium, bool>> ex_search= (x => x.UserId.Equals(UserId) &&(!lastId.HasValue?true: x.Id < lastId)); ;
            return ex_search;
        }
    }
}
