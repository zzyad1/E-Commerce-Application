using E_Commerce.core.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.core.Interfaces.Services
{
    public interface IPaymentService
    {
        public Task<BasketDto> CreateOrUpdatePaymentIntentForExistingOrder(BasketDto basketDto);
        public Task<BasketDto> CreateOrUpdatePaymentIntentForNewOrder(string BasketId);
        public Task<OrderResultDto> UpdatePaymentStatusFieled(string PaymentId);
        public Task<OrderResultDto> UpdatePaymentStatusSuccessed(string PaymentId);

    }
}
