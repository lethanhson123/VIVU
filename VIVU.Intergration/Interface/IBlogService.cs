using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIVU.Intergration.Interface
{
    public interface IBlogService
    {
        Task<CommonResponseModel<IEnumerable<BlogModel>>> GetWithQuery(BlogQueryModel request, string token = "");
        Task<CommonResponseModel<IEnumerable<BlogModel>>> Get(string keywords, string token = "");
        Task<CommonResponseModel<BlogDetailModel>> Get(int Id);
        Task<CommonResponseModel<BlogModel>> Create(BlogDetailModel request, string token = "");
        Task<CommonResponseModel<BlogModel>> Update(BlogDetailModel request, string token = "");
        Task<CommonResponseModel<object>> Delete(int Id, string token = "");
    }
}
