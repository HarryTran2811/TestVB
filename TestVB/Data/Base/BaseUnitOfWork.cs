using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace TestVB.Data.Base
{
    public abstract class BaseUnitOfWork : IDisposable
    {
        private bool _disposedValue;
        private readonly DbContext _dbContext;

        public BaseUnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public DbContext DbContext
        {
            get
            {
                return _dbContext;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                    _dbContext.Dispose();

                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public virtual void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}