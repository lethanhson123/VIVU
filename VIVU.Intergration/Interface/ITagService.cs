using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIVU.Intergration.Interface
{
    public interface ITagService
    {
        Task<CommonResponseModel<IEnumerable<TagModel>>> GetAll(string keywords = "", string token = "");
        Task<CommonResponseModel<IEnumerable<TagModel>>> GetWithQuery(TagQueryModel request, string token = "");
        Task<CommonResponseModel<TagDetailModel>> Get(int Id);
        Task<CommonResponseModel<TagModel>> Create(TagModel request, string token = "");
        Task<CommonResponseModel<TagModel>> Update(TagModel request, string token = "");
        Task<CommonResponseModel<object>> Delete(int Id, string token = "");
    }
}
