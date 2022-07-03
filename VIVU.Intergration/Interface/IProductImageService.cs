
namespace VIVU.Intergration.Interface;

public interface IProductImageService
{
    Task<CommonResponseModel<IEnumerable<ProductImageModel>>> Get(string token = "");
    Task<CommonResponseModel<IEnumerable<ProductImageModel>>> GetWithQuery(ProductImageQueryModel query, string token = "");
    Task<CommonResponseModel<ProductImageModel>> GetDetail(int Id, string token = "");
    Task<CommonResponseModel<ProductImageModel>> Create(ProductImageModel request, string token = "");
    Task<CommonResponseModel<object>> Update(ProductImageModel request, string token = "");
    Task<CommonResponseModel<object>> Delete(int Id, string token = "");

}
