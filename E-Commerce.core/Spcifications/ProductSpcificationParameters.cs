using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.core.Spcifications
{
    public class ProductSpcificationParameters
    {
        private const int MaxPageSize = 10;
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public ProductSpcification? Sort { get; set; }

        public int PageIndex { get; set; } = 1;

        private int _pageSize;
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value > MaxPageSize ? MaxPageSize: value; }
        }

        private string? _search;

        public string? Search
        {
            get { return _search; }
            set { _search = value?.Trim().ToLower(); }
        }


    }
}
