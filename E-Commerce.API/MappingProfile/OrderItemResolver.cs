using AutoMapper;
using AutoMapper.Execution;
using E_Commerce.core.DataTransferObjects;
using E_Commerce.core.Entities;
using E_Commerce.core.Entities.Order;

namespace E_Commerce.API.MappingProfile
{
    public class OrderItemResolver : IValueResolver<OrderItem ,OrderItemDto ,string>
    {
        private readonly IConfiguration _configuration;

        public OrderItemResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
            => !string.IsNullOrWhiteSpace(source.OrderItemProduct.ProductUrl) ? $"{_configuration["BaseUrl"]}{source.OrderItemProduct.ProductUrl}" : string.Empty;

    }
}
