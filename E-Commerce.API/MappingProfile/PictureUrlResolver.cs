using AutoMapper;
using E_Commerce.core.DataTransferObjects;
using E_Commerce.core.Entities;

namespace E_Commerce.API.MappingProfile
{
    public class PictureUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration _configuration;

        public PictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context) 
            => !string.IsNullOrWhiteSpace(source.PictureUrl) ? $"{_configuration["BaseUrl"]}{source.PictureUrl}" : string.Empty;
    }
}
