
namespace VIVU.Intergration.Interface;

public interface IProductCategoryService
{
    Task<CommonResponseModel<IEnumerable<ProductCategoryModel>>> Get(string token = "");
    Task<CommonResponseModel<IEnumerable<ProductCategoryModel>>> GetWithQuery(ProductCategoryQueryModel query, string token = "");
    Task<CommonResponseModel<ProductCategoryModel>> GetDetail(string Id, string token = "");
    Task<CommonResponseModel<ProductCategoryModel>> Create(ProductCategoryModel request, string token = "");
    Task<CommonResponseModel<object>> Update(ProductCategoryModel request, string token = "");
    Task<CommonResponseModel<object>> Delete(string Id, string token = "");

}
