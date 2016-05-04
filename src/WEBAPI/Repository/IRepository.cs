using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WEBAPI.Repository
{
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
