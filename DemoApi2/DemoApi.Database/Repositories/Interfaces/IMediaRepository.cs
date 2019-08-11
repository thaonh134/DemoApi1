using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DemoApi.Database.Base;
using DemoApi.Database.DatabaseContext;

namespace DemoApi.Database.Repositories.Interfaces
{
    public interface IMediaRepository :IRepository<Medium>
    {
        Expression<Func<Medium, bool>> ExpressionSearch(string UserId,int? lastId);
    }
}
