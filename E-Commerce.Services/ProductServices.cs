using AutoMapper;
using E_Commerce.core.DataTransferObjects;
using E_Commerce.core.Entities;
using E_Commerce.core.Interfaces.Repositories;
using E_Commerce.core.Interfaces.Services;
using E_Commerce.core.Spcifications;
using Ecommerce.Repository.Spcifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services
{
    public class ProductServices : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductServices(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginatedDataDto<ProductToReturnDto>> GetAllProductAsync(ProductSpcificationParameters specs)
        {
            var spec = new ProductSpcifications(specs);
            var Product = await _unitOfWork.Repository<Product, int>().GetAllWithSpecAsync(spec);
            var specCount = new ProductCountWithSpec(specs);
            var count = await _unitOfWork.Repository<Product, int>().GetProductCountWithSpec(specCount);
            var MappedProduct = _mapper.Map<IReadOnlyList<ProductToReturnDto>>(Product);
            return new PaginatedDataDto<ProductToReturnDto>
            {
                Data = MappedProduct,
                PageIndex = specs.PageIndex,
                PageSize = specs.PageSize,
                TotalCount = count
            };
        }

        public async Task<IEnumerable<BrandTypeDto>> GetAllBrandsAsync()
        {
            var Brands = await _unitOfWork.Repository<ProductBrand, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<BrandTypeDto>>(Brands);
        }

        public async Task<IEnumerable<BrandTypeDto>> GetAllTypesAsync()
        {
            var Types = await _unitOfWork.Repository<ProductType, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<BrandTypeDto>>(Types);
        }

        public async Task<ProductToReturnDto> GetProductAsync(int id)
        {
            var spec = new ProductSpcifications(id);
            var Product = await _unitOfWork.Repository<Product, int>().GetWithSpecAsync(spec);
            return _mapper.Map<ProductToReturnDto>(Product);
        }
    }
}
