﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.core.Entities
{
    public class ProductBrand : BaseEntity<int>
    {
        public string Name { get; set; }

    }
}
