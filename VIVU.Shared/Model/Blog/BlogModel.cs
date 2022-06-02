using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIVU.Shared.Model
{
    public class BlogModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Meta { get; set; } = string.Empty;
        public string Keywords { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string AuthorId { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public DateTime PostDate { get; set; }
        public bool IsPublished { get; set; }
    }
}
