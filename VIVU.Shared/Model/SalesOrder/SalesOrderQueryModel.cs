using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIVU.Shared.Model
{
    public class SalesOrderQueryModel
    {
        public string? Keywords { get; set; }
        public int PageIndex { get; set; }
        public int Limit { get; set; }
    }
}
