using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace TestVB.Data.Base
{
    public class BaseRepository<T> : IRepository<T> where T : class, IBase, new()
    {
        private DbContext _dbContext;
        private DbSet<T> _dbSet;

        public BaseRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }
        // Generic query method
        public virtual IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = ""
    )
        {
            IQueryable<T> query = _dbSet;

            if ((filter != null))
                query = query.Where(filter);

            foreach (string includeProperty in includeProperties.Split(new char[] { }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);

            if (orderBy != null)
                return orderBy(query).ToList();
            else
                return query.ToList();
        }

        // Get record by ID
        public virtual T GetByID(object id)
        {
            return _dbSet.Find(id);
        }

        // Insert new record
        public virtual void Insert(T entity)
        {
            _dbSet.Add(entity);
        }

        // Delete record by ID
        public virtual void Delete(object id)
        {
            T entityToDelete = _dbSet.Find(id);
            Delete(entityToDelete);
        }

        // Delete record
        public virtual void Delete(T entityToDelete)
        {
            if (_dbContext.Entry(entityToDelete).State == EntityState.Detached)
                _dbSet.Attach(entityToDelete);

            _dbSet.Remove(entityToDelete);
        }

        public virtual void Update(T entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _dbContext.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}