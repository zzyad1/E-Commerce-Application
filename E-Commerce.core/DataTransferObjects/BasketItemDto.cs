using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.core.DataTransferObjects
{
    public class BasketItemDto
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        [Range(1, 99)]
        public int Quntity { get; set; }
        [Required]
        public string PictureUrl { get; set; }
        [Required]
        public string TypeName { get; set; }
        [Required]
        public string BrandName { get; set; }
    }
}
