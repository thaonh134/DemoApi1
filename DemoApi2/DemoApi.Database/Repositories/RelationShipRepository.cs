using DemoApi.Common.Enums;
using DemoApi.Common.Pagination;
using DemoApi.Database.Base;
using DemoApi.Database.DatabaseContext;
using DemoApi.Database.Repositories.Interfaces;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Database.Repositories
{
    public class RelationShipRepository : BaseRepositoryDateModify<RelationShip, test15_api_everEntities>, IRelationShipRepository
    {
        public RelationShipRepository(test15_api_everEntities dbContext) : base(dbContext)
        {

        }
        public Expression<Func<AspNetUser, bool>> ExpressionSearch( string username)
        {
            Expression<Func<AspNetUser, bool>> ex_search;
            //if (lastId.HasValue)         ex_search = (x => x.UserId.Equals(UserId) && x.Id < lastId);
            //else ex_search = (x => x.UserId.Equals(UserId) );
            if (!string.IsNullOrEmpty(username)) ex_search= (x => x.LastName.Equals(username));
           return null;
        }
        public async Task<PaginationAndDataResult<AspNetUser>> GetAllUserInRelation(PaginationRequest paginationRequest, string UserId, Expression<Func<AspNetUser, bool>> filter, Func<IQueryable<AspNetUser>, IOrderedQueryable<AspNetUser>> orderCondition = null)
        {
            int totalItemCount = -1;
            List<AspNetUser> entities;
            //user create posts
            var queryUserOne = from ep in this._dbContext.RelationShips
                               join e in _dbContext.AspNetUsers on ep.User_One_Id equals e.Id
                               where ep.User_Two_Id == UserId
                               select e;


            //group posts that user is a part of
            var queryUserTwo = from ep in this._dbContext.RelationShips
                               join e in _dbContext.AspNetUsers on ep.User_Two_Id equals e.Id
                               where ep.User_One_Id == UserId
                               select e;


            //create union
            var query = queryUserOne.Union(queryUserTwo).Distinct();

            if (orderCondition != null)
            {
                query = orderCondition(query);
            }
            if (filter != null)
            {
                query = query.AsExpandable().Where(filter);
            }

            if (paginationRequest.PageNumber == -1)
            {
                totalItemCount = await query.CountAsync();
                entities = await query.ToListAsync();
            }
            else
            {
                totalItemCount = await query.CountAsync();
                query = query.Skip(paginationRequest.StartIndex).Take(paginationRequest.PageSize);
                entities = await query.ToListAsync();
            }
            PaginationAndDataResult<AspNetUser> result = new PaginationAndDataResult<AspNetUser>(entities, paginationRequest, totalItemCount);
            return result;


        }

      
    }
}
