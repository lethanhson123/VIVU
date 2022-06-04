using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIVU.Data.Entities
{
    public class Product : CommonAudit
    {
        public int Id { get; set; } 
        public string Name { get; set; } = string.Empty;
        public string SKU { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Keywords { get; set; } = string.Empty;
        public decimal Price { get; set; } 
        public string Tags { get; set; } = string.Empty;
        public string Meta { get; set; } = string.Empty;
    }
}
