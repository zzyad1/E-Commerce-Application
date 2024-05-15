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
    public class ProductSpcifications : BaseSpcification<Product>
    {
        public ProductSpcifications(ProductSpcificationParameters spcific)
        : base(product => 
            (!spcific.BrandId.HasValue || product.BrandId ==spcific.BrandId.Value)&&
            (!spcific.TypeId.HasValue || product.TypeId == spcific.TypeId.Value)&&
            (string.IsNullOrWhiteSpace(spcific.Search) || product.Name.ToLower().Contains(spcific.Search))) 
        {
            IncludeExpression.Add(product=> product.ProductBrand);
            IncludeExpression.Add(product=> product.ProductType);
            ApplyPagination(spcific.PageSize, spcific.PageIndex);
            if (spcific.Sort is not null)
            {
                switch(spcific.Sort)
                {
                    case ProductSpcification.NameAsc:
                        OrderBy = x => x.Name;
                        break;
                    case ProductSpcification.NameDesc:
                        OrderByDesc = x => x.Name;
                        break;
                    case ProductSpcification.PriceAsc:
                        OrderBy = x => x.Price;
                        break;
                    case ProductSpcification.PriceDesc:
                        OrderByDesc = x => x.Price;
                        break;
                    default:
                        OrderBy = x => x.Name;
                        break;
                }
                
            }
            else
            {
                OrderBy = x => x.Name;
            }
        }
        public ProductSpcifications(int id) 
            : base(product => product.Id == id)
        {
            IncludeExpression.Add(product => product.ProductBrand);
            IncludeExpression.Add(product => product.ProductType);
        }
    }
}
