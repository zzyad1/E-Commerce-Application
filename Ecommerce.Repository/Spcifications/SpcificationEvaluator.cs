using E_Commerce.core.Entities;
using E_Commerce.core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repository.Spcifications
{
    public static class SpcificationEvaluator<TEntity , T> where TEntity : BaseEntity<T>
    {
        public static IQueryable<TEntity> BuildQuery(IQueryable<TEntity> inputQuery ,ISpecification<TEntity> specification)
        {
            var query = inputQuery;

            if (specification.Criteria is not null)
                query = query.Where(specification.Criteria);

            if (specification.OrderBy is not null)
                query = query.OrderBy(specification.OrderBy);
            
            if (specification.OrderByDesc is not null)
                query = query.OrderByDescending(specification.OrderByDesc);

            if (specification.IsPaginated)
                query = query.Skip(specification.Skip).Take(specification.Take);
            

            query = specification.IncludeExpression
                .Aggregate(query ,(currentQuery ,expression)=> currentQuery.Include(expression));
            return query;
        }
    }
}
