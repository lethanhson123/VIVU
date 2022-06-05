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
        public string ProductSku { get; set; } = string.Empty;

    }
}
