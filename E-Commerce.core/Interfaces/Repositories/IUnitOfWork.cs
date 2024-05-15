using E_Commerce.core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.core.Interfaces.Repositories
{
    public interface IUnitOfWork :IAsyncDisposable
    {
        IGenaricRepository<TEntity ,T> Repository<TEntity ,T>() where TEntity : BaseEntity<T>;

        Task<int> CompleteAsync();

    }
}
