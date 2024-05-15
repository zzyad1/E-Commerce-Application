using E_Commerce.core.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.core.Interfaces.Services
{
    public interface IBasketServices
    {
        Task<BasketDto?> GetBasketAsync(string id);
        Task<BasketDto?> UpdateBasketAsync(BasketDto basket);
        Task<bool> DeleteBasketAsync(string id);
    }
}
