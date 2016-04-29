using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WEBAPI.Models;

namespace WEBAPI.Repository
{
    public interface IArticleRepositoryReadOnly
    {
        Task<IEnumerable<Article>> GetAsync(Func<IQueryable<Article>, IQueryable<Article>> queryShaper, CancellationToken cancellationToken);
        Task<TResult> GetAsync<TResult>(Func<IQueryable<Article>, TResult> queryShaper, CancellationToken cancellationToken);
    }
}
