using DemoApi.Common.Pagination;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using LinqKit;
namespace DemoApi.Database.Base
{
    /// <summary>
    /// BaseRepository
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class BaseRepository<TEntity, TContext> : IDisposable, IRepository<TEntity> where TEntity : class, new() where TContext : DbContext
    {
        protected TContext _dbContext;
        protected DbSet<TEntity> _dbSet;

        /// <summary>
        /// BaseRepository
        /// </summary>
        /// <param name="dbContext"></param>
        public BaseRepository(TContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Create(TEntity entity)
        {
            _dbSet.Add(entity);
            _dbContext.Entry(entity).State = EntityState.Added;
        }

        /// <summary>
        /// Create Range
        /// </summary>
        /// <param name="entity"></param>
        public virtual void CreateRange(List<TEntity> entities)
        {
            _dbSet.AddRange(entities);
            foreach (TEntity item in entities)
            {
                _dbContext.Entry(item).State = EntityState.Added;
            }
        }
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Delete(TEntity entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            _dbSet.Remove(entity);
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            return _dbSet.FirstOrDefault(predicate);
        }
        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            return await _dbSet.FirstOrDefaultAsync(predicate);
        }
        /// <summary>
        /// Get
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual List<TEntity> GetMulti(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            return _dbSet.Where(predicate).ToList();
        }
        /// <summary>
        /// Get
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual List<TEntity> GetMulti(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderCondition = null)
        {
            IQueryable<TEntity> query = _dbSet;
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }
            if (orderCondition != null)
            {
                query = orderCondition(query);
            }
            return query.Where(predicate).ToList();
        }
        public async Task<List<TEntity>> GetMultiAsync(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<List<TEntity>> GetMultiAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderCondition = null)
        {
            IQueryable<TEntity> query = _dbSet;
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }
            if (orderCondition != null)
            {
                query = orderCondition(query);
            }
            return await query.Where(predicate).ToListAsync();
        }

        /// <summary>
        /// GetAll
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual TEntity Get(object id)
        {
            return _dbSet.Find(id);
        }

        public virtual Task<TEntity> GetAsync(object id)
        {
            return _dbSet.FindAsync(id);
        }

        /// <summary>
        /// GetAll
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<TEntity> GetAll()
        {
            return _dbSet;
        }

        /// <summary>
        /// GetAll
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            return _dbSet.Where(predicate);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual DbRawSqlQuery<T> ExecuteSql<T>(string sqlCommand, params object[] parameters)
        {
            return _dbContext.Database.SqlQuery<T>(sqlCommand, parameters);
        }

        public int Sum(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, int?>> selector)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            return _dbSet.Where(predicate).Sum(selector) ?? 0;
        }

        public async Task<int> SumAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, int?>> selector)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            return await _dbSet.Where(predicate).SumAsync(selector) ?? 0;
        }

        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            return _dbSet.Where(predicate).Count();
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            return await _dbSet.Where(predicate).CountAsync();
        }


        public PaginationAndDataResult<TEntity> SelectPage(PaginationRequest paginationRequest, Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderCondition = null)
        {
            int totalItemCount = -1;
            IQueryable<TEntity> query = _dbSet;
            List<TEntity> entities;
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
                totalItemCount = query.Count();
                entities = query.ToList();
            }
            else
            {
                totalItemCount = GetAll(filter).Count();
                entities = query.Skip(paginationRequest.StartIndex).Take(paginationRequest.PageSize).ToList();
            }

            PaginationAndDataResult<TEntity> result = new PaginationAndDataResult<TEntity>(entities, paginationRequest, totalItemCount);
            return result;
        }

        public async Task<PaginationAndDataResult<TEntity>> SelectPageAsync(PaginationRequest paginationRequest, Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderCondition = null)
        {
            int totalItemCount = -1;
            IQueryable<TEntity> query = _dbSet;
            List<TEntity> entities;
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
            PaginationAndDataResult<TEntity> result = new PaginationAndDataResult<TEntity>(entities, paginationRequest, totalItemCount);
            return result;
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public void Dispose()
        {
            if (_dbContext != null)
            {
                _dbContext.Dispose();
                _dbContext = null;
            }
        }
    }
}
