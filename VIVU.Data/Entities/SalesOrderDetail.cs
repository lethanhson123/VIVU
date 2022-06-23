using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIVU.Data.Entities
{
    public class SalesOrderDetail : CommonAudit
    {
        public int Id { get; set; }
        public string SalesOrderId { get; set; } = string.Empty;
        public string ProductId { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; } // Giá bán
        public decimal DeliverPrice { get; set; } // Giá bán cuối cùng
        public decimal TotalPrice { get; set; }

    }
}
