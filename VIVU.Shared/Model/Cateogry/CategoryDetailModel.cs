using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIVU.Shared.Model
{
    public class CategoryDetailModel : CategoryModel
    {
        public List<BlogModel> Blogs = new List<BlogModel>();
    }
}
