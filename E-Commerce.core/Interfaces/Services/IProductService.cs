using E_Commerce.core.DataTransferObjects;
using E_Commerce.core.Entities;
using E_Commerce.core.Spcifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.core.Interfaces.Services
{
    public interface IProductService
    {
        Task<PaginatedDataDto<ProductToReturnDto>> GetAllProductAsync(ProductSpcificationParameters specs);
        Task<ProductToReturnDto> GetProductAsync(int id);
        Task<IEnumerable<BrandTypeDto>> GetAllBrandsAsync();
        Task<IEnumerable<BrandTypeDto>> GetAllTypesAsync();
    }
}
