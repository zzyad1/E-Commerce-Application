using E_Commerce.core.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repository.Spcifications
{
    public class OrderSpcification : BaseSpcification<Order>
    {
        public OrderSpcification(string email) 
            : base(o=>o.BuyerEmail== email)
        {
            IncludeExpression.Add(o=>o.DeliveryMethod);
            IncludeExpression.Add(o=>o.OrderItems);

            OrderByDesc = o => o.OrderDate;
        }
        
        public OrderSpcification(Guid id ,string email) 
            : base(o=>o.BuyerEmail== email)
        {
            IncludeExpression.Add(o=>o.DeliveryMethod);
            IncludeExpression.Add(o=>o.OrderItems);
        }
    }
}
