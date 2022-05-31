using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIVU.Data.Entities
{
    public class BlogTag : CommonAudit
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
        public int TagId { get; set; }
    }
}
