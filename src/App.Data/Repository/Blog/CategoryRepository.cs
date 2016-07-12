using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using App.Data.Entities.Blog;
using App.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace App.Data.Repository.Blog
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationContext _dbContext;
        public CategoryRepository(ApplicationContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        public void Add(Category item)
        {
            throw new NotImplementedException();
        }

        public void DiscardChanges()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Category>> GetAsync(Func<IQueryable<Category>, IQueryable<Category>> queryShaper, CancellationToken cancellationToken)
        {
            var query = queryShaper(_dbContext.Categories);
            return await query.ToArrayAsync(cancellationToken);
        }

        public async Task<TResult> GetAsync<TResult>(Func<IQueryable<Category>, TResult> queryShaper, CancellationToken cancellationToken)
        {
            var set = _dbContext.Categories;
            var query = queryShaper;
            var factory = Task<TResult>.Factory;
            return await factory.StartNew(() => query(set), cancellationToken);
        }

        public Category GetByPrimaryKey(int id)
        {
            throw new NotImplementedException();
        }

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
