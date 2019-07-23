using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Database.Base
{
    public class BaseRepositoryDateModify<TEntity, TContext> : BaseRepository<TEntity, TContext> where TEntity : class, new() where TContext : DbContext
    {
        public BaseRepositoryDateModify(TContext dbContext) : base(dbContext)
        {
        }

        public override void Create(TEntity entity)
        {
            Type entityType = entity.GetType();
            entityType.GetProperty("CreatedDate").SetValue(entity, DateTime.UtcNow);
            PropertyInfo isDelete = entityType.GetProperty("IsDelete");

            if (isDelete != null)
            {
                isDelete.SetValue(entity, false);
            }
            base.Create(entity);
        }

        public override void Update(TEntity entity)
        {
            Type entityType = entity.GetType();

            PropertyInfo updateAt = entityType.GetProperty("UpdatedDate");

            if (updateAt != null)
            {
                updateAt.SetValue(entity, DateTime.UtcNow);
            }
            base.Update(entity);
        }

        public override void Delete(TEntity entity)
        {
            Type entityType = entity.GetType();
            PropertyInfo isDelete = entityType.GetProperty("IsDelete");

            if (isDelete != null)
            {
                isDelete.SetValue(entity, true);
                base.Update(entity);
            }
            else
            {
                base.Delete(entity);
            }
        }
    }
}
