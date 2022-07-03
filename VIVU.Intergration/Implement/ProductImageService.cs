
namespace VIVU.Intergration.Implement;

public class ProductImageService : IProductImageService
{
    private readonly IHttpClientFactory httpClientFactory;
    private readonly ClientConfig clientConfig;

    public ProductImageService(IHttpClientFactory httpClientFactory,
           IOptions<ClientConfig> clientConfig)
    {
        this.httpClientFactory = httpClientFactory;
        this.clientConfig = clientConfig.Value;
    }
    /// <summary>
    /// get all
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<CommonResponseModel<IEnumerable<ProductImageModel>>> Get(string token = "")
    {
        var response = new CommonResponseModel<IEnumerable<ProductImageModel>>();
        try
        {
            var client = httpClientFactory.CreateClient();
            var headers = new Dictionary<string, string>();
            var path = string.Format("{0}{1}", clientConfig.Host, clientConfig.ProductImage);
            var remoteResponse = client
                .ExecuteGet<CommonResponseModel<IEnumerable<ProductImageModel>>>(
                   path, null, headers);
            response = remoteResponse.Result.Data ??
                new CommonResponseModel<IEnumerable<ProductImageModel>>();
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }
        return Task.FromResult(response);
    }
    /// <summary>
    /// get with query
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<CommonResponseModel<IEnumerable<ProductImageModel>>> GetWithQuery(ProductImageQueryModel request, string token = "")
    {

        var response = new CommonResponseModel<IEnumerable<ProductImageModel>>();
        try
        {
            var client = httpClientFactory.CreateClient();
            var headers = new Dictionary<string, string>();
            var path = string.Format("{0}{1}?{2}", clientConfig.Host, clientConfig.ProductImageGetWithQuery, request.ToQueryStringData());

            var remoteResponse = client
                .ExecuteGet<CommonResponseModel<IEnumerable<ProductImageModel>>>(
                   path, null, headers);
            response = remoteResponse.Result.Data ??
                new CommonResponseModel<IEnumerable<ProductImageModel>>();
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }
        return Task.FromResult(response);
    }

    public Task<CommonResponseModel<ProductImageModel>> GetDetail(int Id,string token)
    {
        var response = new CommonResponseModel<ProductImageModel>();
        try
        {
            var client = httpClientFactory.CreateClient();
            var headers = new Dictionary<string, string>();
            var path = string.Format("{0}{1}/{2}", clientConfig.Host, clientConfig.ProductImage, Id);
            var remoteResponse = client
                .ExecuteGet<CommonResponseModel<ProductImageModel>>(
                   path, null, headers);
            response = remoteResponse.Result.Data ??
                new CommonResponseModel<ProductImageModel>();
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }
        return Task.FromResult(response);
    }
    public Task<CommonResponseModel<ProductImageModel>> Create(ProductImageModel request, string token ="")
    {
        var response = new CommonResponseModel<ProductImageModel>();

        try
        {
            var client = httpClientFactory.CreateClient();
            var headers = new Dictionary<string, string>();
            var path = string.Format("{0}{1}", clientConfig.Host, clientConfig.ProductImage);

            headers.Add("Authorization","Bearer " + token);
            var remoteResponse = client.ExecutePost<CommonResponseModel<ProductImageModel>>(
                path, request, null, headers);
            response = remoteResponse.Result.Data ??
                new CommonResponseModel<ProductImageModel>();
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }

        return Task.FromResult(response);
    }
    public Task<CommonResponseModel<object>> Update(ProductImageModel request, string token = "") {
        var response = new CommonResponseModel<object>();

        try
        {
            var client = httpClientFactory.CreateClient();
            var headers = new Dictionary<string, string>();
            var path = string.Format("{0}{1}/{2}", clientConfig.Host, clientConfig.ProductImage, request.Id);

            headers.Add("Authorization", "Bearer " + token);
            var remoteResponse = client.ExecutePut<CommonResponseModel<object>>(
               path, request, null, headers);
            response = remoteResponse.Result.Data ??
                new CommonResponseModel<object>();
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }

        return Task.FromResult(response);
    }
    public Task<CommonResponseModel<object>> Delete(int Id, string token = "") {
        var response = new CommonResponseModel<object>();

        try
        {
            var client = httpClientFactory.CreateClient();
            var headers = new Dictionary<string, string>();
            var path = string.Format("{0}{1}/{2}", clientConfig.Host, clientConfig.ProductImage, Id);

            headers.Add("Authorization", "Bearer " + token);
            var remoteResponse = client.ExecuteDelete<CommonResponseModel<object>>(
               path, null, null, headers);
            response = remoteResponse.Result.Data ??
                new CommonResponseModel<object>();
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }

        return Task.FromResult(response);
    }
}
