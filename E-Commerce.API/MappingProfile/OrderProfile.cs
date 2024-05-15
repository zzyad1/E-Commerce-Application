using AutoMapper;
using E_Commerce.core.DataTransferObjects;
using E_Commerce.core.Entities.Order;

namespace E_Commerce.API.MappingProfile
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<ShippingAddress ,AddressDto>().ReverseMap();
            CreateMap<OrderItem ,OrderItemDto>()
                .ForMember(d=>d.ProductId , o=>o.MapFrom(s=>s.OrderItemProduct.ProductId))
                .ForMember(d=>d.ProductName , o=>o.MapFrom(s=>s.OrderItemProduct.ProductName))
                .ForMember(d=>d.ProductUrl , o=>o.MapFrom(s=>s.OrderItemProduct.ProductUrl))
                .ForMember(d=>d.ProductUrl , o=>o.MapFrom<OrderItemResolver>());
            CreateMap<Order, OrderResultDto>()
                .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.ShippingPrice, o => o.MapFrom(s => s.DeliveryMethod.Price));
        }
    }
}
