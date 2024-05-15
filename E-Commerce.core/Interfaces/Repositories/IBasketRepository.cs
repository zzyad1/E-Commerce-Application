using E_Commerce.core.Entities.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.core.Interfaces.Repositories
{
    public interface IBasketRepository
    {
        Task<CustomerBasket?> GetCustomerBasketAsync(string id);
        Task<CustomerBasket?> UpdateCustomerBasketAsync(CustomerBasket basket);
        Task<bool> DeleteCustomerBasketAsync(string id);
    }
}
