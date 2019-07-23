using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Database.Base
{
    public class BaseUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        private TContext _context;
        private DbContextTransaction _transaction;
        private IsolationLevel? _isolationLevel;
        private bool _disposed = false;

        public void SetIsolationLevel(IsolationLevel isolationLevel)
        {
            _isolationLevel = isolationLevel;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public BaseUnitOfWork(TContext context)
        {
            _context = context;
        }

        public TContext DbContext
        {
            get
            {
                return _context;
            }
        }

        public void ForceBeginTransaction()
        {
            StartNewTransactionIfNeeded();
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context?.Dispose();
                }
            }

            _disposed = true;
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _transaction = null;

            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public void DetachEntity<TEntity>(TEntity entity) where TEntity : class, new()
        {
            var entry = DbContext.Entry(entity);
            if (entry.State != EntityState.Detached)
                entry.State = EntityState.Detached;
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public int SaveChangesWithTransaction()
        {
            StartNewTransactionIfNeeded();
            return _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<int> SaveChangesWithTransactionAsync()
        {
            StartNewTransactionIfNeeded();
            return await _context.SaveChangesAsync();
        }

        private void StartNewTransactionIfNeeded()
        {
            if (_transaction == null)
            {
                _transaction = _isolationLevel.HasValue
                    ? _context.Database.BeginTransaction(_isolationLevel.GetValueOrDefault())
                    : _context.Database.BeginTransaction();
            }
        }
        public void CommitTransaction()
        {
            //do not open transaction here, because if during the request
            //nothing was changed (only select queries were run), we don't
            //want to open and commit an empty transaction - calling SaveChanges()
            //on _transactionProvider will not send any sql to database in such case
            _context.SaveChanges();

            if (_transaction != null)
            {
                _transaction.Commit();

                _transaction.Dispose();
                _transaction = null;
            }
        }

        public void RollbackTransaction()
        {
            if (_transaction == null) return;

            _transaction.Rollback();

            _transaction.Dispose();
            _transaction = null;
        }
    }
}
