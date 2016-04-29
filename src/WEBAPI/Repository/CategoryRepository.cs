using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WEBAPI.Models;

namespace WEBAPI.Repository
{
    public abstract class CategoryRepository : IRepository<Category>
    {
        ApplicationDbContext _dbContext;
        public CategoryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public bool HasPendingChanges
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Add(Category item)
        {
            _dbContext.Categories.Add(item);
            _dbContext.SaveChanges();
        }

        public void DiscardChanges()
        {
            throw new NotImplementedException();
        }

        public abstract Task<IEnumerable<Category>> GetAsync(Func<IQueryable<Category>, IQueryable<Category>> queryShaper, CancellationToken cancellationToken);

        public abstract Task<TResult> GetAsync<TResult>(Func<IQueryable<Category>, TResult> queryShaper, CancellationToken cancellationToken);

        public void Remove(Category item)
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Update(Category item)
        {
            throw new NotImplementedException();
        }
    }
}
