using E_Commerce.core.Entities;
using E_Commerce.core.Interfaces.Repositories;
using Ecommerce.Repository.DataContext;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ECommerceDataContext _context;
        private readonly Hashtable _repositry;
        public UnitOfWork(ECommerceDataContext context)
        {
            _context = context;
            _repositry = new Hashtable();
        }
        public IGenaricRepository<TEntity, T> Repository<TEntity, T>() where TEntity : BaseEntity<T>
        {
            var typeName = typeof(TEntity).Name;
            if(!_repositry.ContainsKey(typeName))
            {
                var repo = new GenaricRepository<TEntity, T>(_context);
                _repositry.Add(typeName, repo);
                return repo; 
            }
            return (_repositry[typeName] as GenaricRepository<TEntity, T>)!;

        }
        public async Task<int> CompleteAsync()=> await _context.SaveChangesAsync();

        public async ValueTask DisposeAsync()=> await _context.DisposeAsync();

    }
}
