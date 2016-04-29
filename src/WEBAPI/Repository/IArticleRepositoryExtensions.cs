using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using WEBAPI.Models;
using WEBAPI.Repository;

namespace WEBAPI.Repository
{
    public static class IArticleRepositoryExtensions
    {
        public static Task<IEnumerable<Article>> GetAsync<Article>(this IArticleRepositoryReadOnly repository, Func<IQueryable<Article>, IQueryable<Article>> queryShaper)
        {
            Contract.Requires(repository != null);
            Contract.Requires(queryShaper != null);
            Contract.Ensures(Contract.Result<Task<IEnumerable<Article>>>() != null);
            return repository.GetAsync(queryShaper, CancellationToken.None);
        }

        public static Task<Article> GetAsync<Article, TResult>(this IArticleRepositoryReadOnly repository, Func<IQueryable<Article>, TResult> queryShaper)
        {
            Contract.Requires(repository != null);
            Contract.Requires(queryShaper != null);
            Contract.Ensures(Contract.Result<Task<TResult>>() != null);
            return repository.GetAsync(queryShaper, CancellationToken.None);
        }

        //public static Task<IEnumerable<T>> GetAllAsync<T>(this IArticleRepository repository) where T : class
        //{
        //    Contract.Requires(repository != null);
        //    Contract.Ensures(Contract.Result<Task<IEnumerable<T>>>() != null);
        //    return repository.GetAsync(q => q, CancellationToken.None);
        //}

        //public static Task<IEnumerable<T>> GetAllAsync<T>(this IArticleRepository repository, CancellationToken cancellationToken) where T : class
        //{
        //    Contract.Requires(repository != null);
        //    Contract.Ensures(Contract.Result<Task<IEnumerable<T>>>() != null);
        //    return repository.GetAsync(q => q, cancellationToken);
        //}
    }
}
