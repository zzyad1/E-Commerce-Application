using E_Commerce.core.Interfaces;
using System.Linq.Expressions;

namespace Ecommerce.Repository.Spcifications
{
    public class BaseSpcification<T> : ISpecification<T>
    {
        public Expression<Func<T, bool>> Criteria { get;}

        public BaseSpcification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public List<Expression<Func<T, object>>> IncludeExpression { get; } = new();
        public Expression<Func<T, object>> OrderByDesc { get; protected set; }
        public Expression<Func<T, object>> OrderBy { get; protected set; }
        public int Skip { get; protected set; }
        public int Take { get; protected set; }
        public bool IsPaginated { get; protected set; }

        protected void ApplyPagination(int pageSize , int pageIndex)
        {
            IsPaginated = true;
            Take = pageSize;
            Skip = (pageIndex - 1) * pageSize;
        }
    }
}
