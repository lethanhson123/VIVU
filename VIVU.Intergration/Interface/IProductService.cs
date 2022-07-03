
namespace VIVU.Intergration.Interface;

public interface IProductService
{
    Task<CommonResponseModel<IEnumerable<ProductModel>>> Get(string token = "");
    Task<CommonResponseModel<IEnumerable<ProductModel>>> GetWithQuery(ProductQueryModel query, string token = "");
    Task<CommonResponseModel<ProductModel>> GetDetail(string Id, string token = "");
    Task<CommonResponseModel<ProductModel>> Create(ProductModel request, string token = "");
    Task<CommonResponseModel<object>> Update(ProductModel request, string token = "");
    Task<CommonResponseModel<object>> Delete(string Id, string token = "");

}
