using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIVU.Data.Entities
{
    public class BlogRelated : CommonAudit
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int RelatedId { get; set; }
    }
}
