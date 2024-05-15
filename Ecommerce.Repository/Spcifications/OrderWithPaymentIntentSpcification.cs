using E_Commerce.core.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repository.Spcifications
{
    public class OrderWithPaymentIntentSpcification : BaseSpcification<Order>
    {
        public OrderWithPaymentIntentSpcification(string PaymentId) 
            : base(order=> order.PaymentIntentId == PaymentId)
        {
            IncludeExpression.Add(o => o.DeliveryMethod);
        }
    }
}
