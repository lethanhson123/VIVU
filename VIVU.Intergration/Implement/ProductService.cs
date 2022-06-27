
namespace VIVU.Intergration.Implement;

public class ProductService : IProductService
{
    private readonly IHttpClientFactory httpClientFactory;
    private readonly ClientConfig clientConfig;

    public ProductService(IHttpClientFactory httpClientFactory,
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
    public Task<CommonResponseModel<IEnumerable<ProductModel>>> Get(string token = "")
    {
        var response = new CommonResponseModel<IEnumerable<ProductModel>>();
        try
        {
            var client = httpClientFactory.CreateClient();
            var headers = new Dictionary<string, string>();
            var path = string.Format("{0}{1}", clientConfig.Host, clientConfig.Product);
            headers.Add("Authorization", token);
            var remoteResponse = client
                .ExecuteGet<CommonResponseModel<IEnumerable<ProductModel>>>(
                   path, null, headers);
            response = remoteResponse.Result.Data ??
                new CommonResponseModel<IEnumerable<ProductModel>>();
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
    public Task<CommonResponseModel<IEnumerable<ProductModel>>> Get(ProductQueryModel request, string token = "")
    {

        var response = new CommonResponseModel<IEnumerable<ProductModel>>();
        try
        {
            var client = httpClientFactory.CreateClient();
            var headers = new Dictionary<string, string>();
            var path = string.Format("{0}{1}?{2}", clientConfig.Host, clientConfig.ProductGetWithQuery, request.ToQueryStringData());

            headers.Add("Authorization", token);
            var remoteResponse = client
                .ExecuteGet<CommonResponseModel<IEnumerable<ProductModel>>>(
                   path, null, headers);
            response = remoteResponse.Result.Data ??
                new CommonResponseModel<IEnumerable<ProductModel>>();
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }
        return Task.FromResult(response);
    }

    public Task<CommonResponseModel<ProductModel>> GetDetail(string Id,string token)
    {
        var response = new CommonResponseModel<ProductModel>();
        try
        {
            var client = httpClientFactory.CreateClient();
            var headers = new Dictionary<string, string>();
            var path = string.Format("{0}{1}/{2}", clientConfig.Host, clientConfig.Product, Id);

            headers.Add("Authorization",token);
            var remoteResponse = client
                .ExecuteGet<CommonResponseModel<ProductModel>>(
                   path, null, headers);
            response = remoteResponse.Result.Data ??
                new CommonResponseModel<ProductModel>();
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }
        return Task.FromResult(response);
    }
    public Task<CommonResponseModel<ProductModel>> Create(ProductModel request, string token ="")
    {
        var response = new CommonResponseModel<ProductModel>();

        try
        {
            var client = httpClientFactory.CreateClient();
            var headers = new Dictionary<string, string>();
            var path = string.Format("{0}{1}", clientConfig.Host, clientConfig.Product);

            headers.Add("Authorization",token);
            var remoteResponse = client.ExecutePost<CommonResponseModel<ProductModel>>(
                path, request, null, headers);
            response = remoteResponse.Result.Data ??
                new CommonResponseModel<ProductModel>();
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }

        return Task.FromResult(response);
    }
    public Task<CommonResponseModel<object>> Update(ProductModel request, string token = "") {
        var response = new CommonResponseModel<object>();

        try
        {
            var client = httpClientFactory.CreateClient();
            var headers = new Dictionary<string, string>();
            var path = string.Format("{0}{1}/{2}", clientConfig.Host, clientConfig.Product,request.Id);

            headers.Add("Authorization", token);
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
    public Task<CommonResponseModel<object>> Delete(string Id, string token = "") {
        var response = new CommonResponseModel<object>();

        try
        {
            var client = httpClientFactory.CreateClient();
            var headers = new Dictionary<string, string>();
            var path = string.Format("{0}{1}/{2}", clientConfig.Host, clientConfig.Product, Id);

            headers.Add("Authorization",token);
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
