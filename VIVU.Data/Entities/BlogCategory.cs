using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIVU.Data.Entities
{
    public class BlogCategory : CommonAudit
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int PostId { get; set; }
    }
}
