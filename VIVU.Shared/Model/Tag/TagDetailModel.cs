using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIVU.Shared.Model
{
    public class TagDetailModel : TagModel
    {
        public List<BlogModel> Posts { get; set; }
            = new List<BlogModel>();
    }
}
