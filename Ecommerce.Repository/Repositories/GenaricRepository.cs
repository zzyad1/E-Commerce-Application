using E_Commerce.core.Entities;
using E_Commerce.core.Interfaces;
using E_Commerce.core.Interfaces.Repositories;
using Ecommerce.Repository.DataContext;
using Ecommerce.Repository.Spcifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repository.Repositories
{
    public class GenaricRepository<TEntity, T> : IGenaricRepository<TEntity, T> where TEntity : BaseEntity<T>
    {
        private readonly ECommerceDataContext _context;

        public GenaricRepository(ECommerceDataContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TEntity entity) => await _context.Set<TEntity>().AddAsync(entity);

        public void Delete(TEntity entity) => _context.Set<TEntity>().Remove(entity);

        public async Task<IEnumerable<TEntity>> GetAllAsync()=> await _context.Set<TEntity>().ToListAsync();

        public async Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecification<TEntity> specification) =>
            await SpcificationEvaluator<TEntity, T>.BuildQuery(_context.Set<TEntity>(), specification).ToListAsync();

        public async Task<TEntity> GetAsync(T id)=> await _context.Set<TEntity>().FindAsync(id);

        public async Task<int> GetProductCountWithSpec(ISpecification<TEntity> specification)
        => await SpcificationEvaluator<TEntity, T>.BuildQuery(_context.Set<TEntity>(), specification).CountAsync();

        public async Task<TEntity> GetWithSpecAsync(ISpecification<TEntity> specification) 
        => await SpcificationEvaluator<TEntity, T>.BuildQuery(_context.Set<TEntity>(), specification).FirstOrDefaultAsync();


        public void Update(TEntity entity) => _context.Set<TEntity>().Update(entity);
    }
}
