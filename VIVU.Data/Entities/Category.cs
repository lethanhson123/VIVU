using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIVU.Data.Entities
{
    public class Category : CommonAudit
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Meta { get; set; } = string.Empty;
        public string Keywords { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int SortOrder { get; set; }
    }
}
