using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Database.Base
{
    /// <summary>
    /// IUnitOfWork
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Detach a Entity
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        void DetachEntity<TEntity>(TEntity entity) where TEntity : class, new();
        /// <summary>
        /// Opens a new transaction instantly when being called.
        /// If a transaction is already open, it won't do anything.
        /// Generally, you shouldn't call this method unless you need
        /// to control the exact moment of opening a transaction.
        /// Unit of Work automatically handles opening transactions
        /// in a convenient time.        
        /// </summary>
        void ForceBeginTransaction();

        /// <summary>
        /// Commits the current transaction (does nothing when none exists).
        /// </summary>
        void CommitTransaction();

        /// <summary>
        /// Rolls back the current transaction (does nothing when none exists).
        /// </summary>
        void RollbackTransaction();

        /// <summary>
        /// Saves changes to database, previously opening a transaction
        /// only when none exists. The transaction is opened with isolation
        /// level set in Unit of Work before calling this method.
        /// </summary>
        int SaveChangesWithTransaction();

        Task<int> SaveChangesWithTransactionAsync();

        int SaveChanges();

        Task<int> SaveChangesAsync();

        /// <summary>
        /// Sets the isolation level for new transactions.
        /// </summary>
        /// <param name="isolationLevel"></param>
        void SetIsolationLevel(IsolationLevel isolationLevel);
    }
}
