using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIVU.Shared.Model
{
    public class ProductModel
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string SKU { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Keywords { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal PurchasePrice { get; set; } // giá nhập
        public decimal DeliverPrice { get; set; } // Giá bán cuối cùng
        public string Tags { get; set; } = string.Empty;
        public string Meta { get; set; } = string.Empty;
        public string Thumbnail { get; set; } = string.Empty;
    }
}
