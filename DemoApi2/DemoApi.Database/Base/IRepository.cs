using DemoApi.Common.Pagination;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Database.Base
{
    /// <summary>
    /// IRepository
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        void Create(TEntity entity);
        void CreateRange(List<TEntity> entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        TEntity Get(object id);
        Task<TEntity> GetAsync(object id);
        TEntity Get(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);
        List<TEntity> GetMulti(Expression<Func<TEntity, bool>> predicate);
        List<TEntity> GetMulti(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderCondition = null);
        Task<List<TEntity>> GetMultiAsync(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> GetMultiAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderCondition = null);
        IQueryable<TEntity> GetAll();
        Task<List<TEntity>> GetAllAsync();
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate);
        int Sum(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, int?>> selector);
        Task<int> SumAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, int?>> selector);
        int Count(Expression<Func<TEntity, bool>> predicate);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);
        DbRawSqlQuery<T> ExecuteSql<T>(string sqlCommand, params object[] parameters);
        PaginationAndDataResult<TEntity> SelectPage(PaginationRequest paginationRequest, Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderCondition = null);
        Task<PaginationAndDataResult<TEntity>> SelectPageAsync(PaginationRequest paginationRequest, Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderCondition = null);
    }
}
