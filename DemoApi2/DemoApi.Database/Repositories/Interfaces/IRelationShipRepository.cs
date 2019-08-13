using DemoApi.Common.Pagination;
using DemoApi.Database.Base;
using DemoApi.Database.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Database.Repositories.Interfaces
{
    public interface IRelationShipRepository : IRepository<RelationShip>
    {
        Expression<Func<AspNetUser, bool>> ExpressionSearch(string UserName);
        Task<PaginationAndDataResult<AspNetUser>> GetAllUserInRelation(PaginationRequest paginationRequest, string UserId, Expression<Func<AspNetUser, bool>> filter, Func<IQueryable<AspNetUser>, IOrderedQueryable<AspNetUser>> orderCondition = null);
    }
}
