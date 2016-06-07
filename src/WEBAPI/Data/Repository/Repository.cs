using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using WEBAPI.Models;

namespace WEBAPI.Data.Entities.Repository
{

    public abstract class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;

        public Repository(ApplicationDbContext dbContext)
        {

            Contract.Requires(dbContext != null);
            this._dbContext = dbContext;
        }

        public abstract Task<IEnumerable<T>> GetAsync(Func<IQueryable<T>, IQueryable<T>> queryShaper, CancellationToken cancellationToken);


        public abstract Task<TResult> GetAsync<TResult>(Func<IQueryable<T>, TResult> queryShaper, CancellationToken cancellationToken);

        public virtual void Add(T item)
        {
            _dbContext.Add(item);
        }

        public virtual void Remove(T item)
        {
            this._dbContext.Remove(item);
        }

        public virtual void Update(T item)
        {
            this._dbContext.Add(item);
        }

        public virtual void DiscardChanges()
        {
            this._dbContext.Dispose();
        }

        public virtual Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            return this._dbContext.SaveChangesAsync();
        }

    }
}
