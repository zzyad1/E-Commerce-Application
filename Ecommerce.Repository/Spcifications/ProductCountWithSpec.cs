using E_Commerce.core.Entities;
using E_Commerce.core.Spcifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repository.Spcifications
{
    public class ProductCountWithSpec : BaseSpcification<Product>
    {
        public ProductCountWithSpec(ProductSpcificationParameters specPrameters) :
            base(product =>
            (!specPrameters.BrandId.HasValue || product.BrandId == specPrameters.BrandId.Value) &&
            (!specPrameters.TypeId.HasValue || product.TypeId == specPrameters.TypeId.Value) &&
            (string.IsNullOrWhiteSpace(specPrameters.Search) || product.Name.ToLower().Contains(specPrameters.Search)))
        {
        }
    }
}
