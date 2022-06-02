using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIVU.Shared.Model
{
    public class BlogRelatedModel
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
        public int RelatedId { get; set; }

        /// <summary>
        /// Related information
        /// </summary>
        public string RelatedTitle { get; set; } = string.Empty;
        public string RelatedMeta { get; set; } = string.Empty;
        public string RelatedImage { get; set; } = string.Empty;
    }
}
