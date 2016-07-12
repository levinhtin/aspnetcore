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
    public class TagsRepository : ITagsRepository
    {
        private readonly ApplicationContext _dbContext;
        public TagsRepository(ApplicationContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        public void Add(Tags item)
        {
            throw new NotImplementedException();
        }

        public void DiscardChanges()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Tags>> GetAsync(Func<IQueryable<Tags>, IQueryable<Tags>> queryShaper, CancellationToken cancellationToken)
        {
            var query = queryShaper(_dbContext.Tags);
            return await query.ToArrayAsync(cancellationToken);
        }

        public async Task<TResult> GetAsync<TResult>(Func<IQueryable<Tags>, TResult> queryShaper, CancellationToken cancellationToken)
        {
            var set = _dbContext.Tags;
            var query = queryShaper;
            var factory = Task<TResult>.Factory;
            return await factory.StartNew(() => query(set), cancellationToken);
        }

        public Tags GetByPrimaryKey(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Tags item)
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Update(Tags item)
        {
            throw new NotImplementedException();
        }
    }
}
