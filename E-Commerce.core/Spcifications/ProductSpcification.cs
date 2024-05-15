using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace E_Commerce.core.Spcifications
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ProductSpcification
    {
        NameAsc,NameDesc,PriceAsc,PriceDesc
    }
}
