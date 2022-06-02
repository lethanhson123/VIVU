using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIVU.Shared.Model
{
    public class BlogDetailModel: BlogModel
    {
        public string Content { get; set; } = string.Empty;
        public List<CategoryModel>? Categories { get; set; } = new List<CategoryModel>();
        public List<TagModel>? Tags { get; set; } = new List<TagModel>();
    }
}
