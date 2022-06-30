using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIVU.Intergration.Interface
{
    public interface ICategoriesService
    {
        Task<CommonResponseModel<IEnumerable<CategoryModel>>> GetAll(string keywords, string token = "");
        Task<CommonResponseModel<IEnumerable<CategoryModel>>> GetWithQuery(CategoryQueryModel request, string token = "");
        Task<CommonResponseModel<CategoryDetailModel>> Get(int Id);
        Task<CommonResponseModel<CategoryModel>> Create(CategoryModel request, string token = "");
        Task<CommonResponseModel<CategoryModel>> Update(CategoryModel request, string token = "");
        Task<CommonResponseModel<object>> Delete(int Id, string token = "");
    }
}
