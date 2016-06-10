using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCore.Data.Repository
{
    //public interface IRepository<T> where T : class
    //{
    //    T Get(int id);
    //    IEnumerable<T> GetAll();
    //    IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

    //    void Add(T entity);
    //    void AddRange(IEnumerable<T> entities);

    //    void Remove(T entity);
    //    void RemoveRange(IEnumerable<T> entities);
    //    void Update(T item);
    //    void DiscardChanges();
    //    Task SaveChangesAsync(CancellationToken cancellationToken);
    //}

    [ContractClass(typeof(IRepositoryContract<>))]
    public interface IRepository<T> : IReadOnlyRepository<T> where T : class
    {

        void Add(T item);

        void Remove(T item);

        void Update(T item);

        void DiscardChanges();

        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
