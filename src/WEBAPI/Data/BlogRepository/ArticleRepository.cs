using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WEBAPI.Data.Entities;

namespace WEBAPI.Data.BlogRepository
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly ApplicationIdentityContext _dbContext;
        public ArticleRepository(ApplicationIdentityContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        public void Add(Article item)
        {
            throw new NotImplementedException();
        }

        public void DiscardChanges()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Article>> GetAsync(Func<IQueryable<Article>, IQueryable<Article>> queryShaper, CancellationToken cancellationToken)
        {
            var query = queryShaper(_dbContext.Articles);
            return await query.ToArrayAsync(cancellationToken);
        }

        public async Task<TResult> GetAsync<TResult>(Func<IQueryable<Article>, TResult> queryShaper, CancellationToken cancellationToken)
        {
            var set = _dbContext.Articles;
            var query = queryShaper;
            var factory = Task<TResult>.Factory;
            return await factory.StartNew(() => query(set), cancellationToken);
        }

        public void Remove(Article item)
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Update(Article item)
        {
            throw new NotImplementedException();
        }
    }
}
