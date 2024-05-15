using E_Commerce.core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.core.Interfaces.Repositories
{
    public interface IGenaricRepository<TEntity ,T> where TEntity : BaseEntity<T>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();   
        Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecification<TEntity> specification);   
        Task<int> GetProductCountWithSpec(ISpecification<TEntity> specification);   
        Task<TEntity> GetAsync(T id);
        Task<TEntity> GetWithSpecAsync(ISpecification<TEntity> specification);
        Task AddAsync(TEntity entity);
        void Delete(TEntity entity);
        void Update(TEntity entity);


    }
}
