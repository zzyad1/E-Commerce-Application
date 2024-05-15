using AutoMapper;
using E_Commerce.core.DataTransferObjects;
using E_Commerce.core.Entities.Basket;
using E_Commerce.core.Interfaces.Repositories;
using E_Commerce.core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services
{
    public class BasketServices : IBasketServices
    {
        private readonly IBasketRepository _repository;
        private readonly IMapper _mapper;
        public BasketServices(IBasketRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> DeleteBasketAsync(string id)=>await _repository.DeleteCustomerBasketAsync(id);

        public async Task<BasketDto?> GetBasketAsync(string id)
        {
            var basket = await _repository.GetCustomerBasketAsync(id);
            return _mapper.Map<BasketDto?>(basket);
        }

        public async Task<BasketDto?> UpdateBasketAsync(BasketDto basket)
        {
            var customer = _mapper.Map<CustomerBasket>(basket);
            var update = await _repository.UpdateCustomerBasketAsync(customer);
            return update is null ? null : _mapper.Map<BasketDto?>(update);
        }
    }
}
