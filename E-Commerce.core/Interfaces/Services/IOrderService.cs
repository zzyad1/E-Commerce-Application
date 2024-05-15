using E_Commerce.core.DataTransferObjects;
using E_Commerce.core.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.core.Interfaces.Services
{
    public interface IOrderService
    {
        public Task<IEnumerable<DeliveryMethod>> GetDeliveryMethodsAsync();
        public Task<OrderResultDto> CreateOrdersAsync(OrderDto orderDto);
        public Task<OrderResultDto> GetOrdersAsync(Guid id ,string Email);
        public Task<IEnumerable<OrderResultDto>> GetAllOrdersAsync(string Email);
    }
}
