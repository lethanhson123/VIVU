
namespace VIVU.Intergration.Implement;

public class ProductCategoryService : IProductCategoryService
{
    private readonly IHttpClientFactory httpClientFactory;
    private readonly ClientConfig clientConfig;

    public ProductCategoryService(IHttpClientFactory httpClientFactory,
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
    public Task<CommonResponseModel<IEnumerable<ProductCategoryModel>>> Get(string token = "")
    {
        var response = new CommonResponseModel<IEnumerable<ProductCategoryModel>>();
        try
        {
            var client = httpClientFactory.CreateClient();
            var headers = new Dictionary<string, string>();
            var path = string.Format("{0}{1}", clientConfig.Host, clientConfig.ProductCategory);
            headers.Add("Authorization", token);
            var remoteResponse = client
                .ExecuteGet<CommonResponseModel<IEnumerable<ProductCategoryModel>>>(
                   path, null, headers);
            response = remoteResponse.Result.Data ??
                new CommonResponseModel<IEnumerable<ProductCategoryModel>>();
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
    public Task<CommonResponseModel<IEnumerable<ProductCategoryModel>>> GetWithQuery(ProductCategoryQueryModel request, string token = "")
    {

        var response = new CommonResponseModel<IEnumerable<ProductCategoryModel>>();
        try
        {
            var client = httpClientFactory.CreateClient();
            var headers = new Dictionary<string, string>();
            var path = string.Format("{0}{1}?{2}", clientConfig.Host, clientConfig.ProductCategoryGetWithQuery, request.ToQueryStringData());

            var remoteResponse = client
                .ExecuteGet<CommonResponseModel<IEnumerable<ProductCategoryModel>>>(
                   path, null, headers);
            response = remoteResponse.Result.Data ??
                new CommonResponseModel<IEnumerable<ProductCategoryModel>>();
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }
        return Task.FromResult(response);
    }

    public Task<CommonResponseModel<ProductCategoryModel>> GetDetail(string Id,string token)
    {
        var response = new CommonResponseModel<ProductCategoryModel>();
        try
        {
            var client = httpClientFactory.CreateClient();
            var headers = new Dictionary<string, string>();
            var path = string.Format("{0}{1}/{2}", clientConfig.Host, clientConfig.ProductCategory, Id);
            var remoteResponse = client
                .ExecuteGet<CommonResponseModel<ProductCategoryModel>>(
                   path, null, headers);
            response = remoteResponse.Result.Data ??
                new CommonResponseModel<ProductCategoryModel>();
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }
        return Task.FromResult(response);
    }
    public Task<CommonResponseModel<ProductCategoryModel>> Create(ProductCategoryModel request, string token ="")
    {
        var response = new CommonResponseModel<ProductCategoryModel>();

        try
        {
            var client = httpClientFactory.CreateClient();
            var headers = new Dictionary<string, string>();
            var path = string.Format("{0}{1}", clientConfig.Host, clientConfig.ProductCategory);

            headers.Add("Authorization","Bearer "+ token);
            var remoteResponse = client.ExecutePost<CommonResponseModel<ProductCategoryModel>>(
                path, request, null, headers);
            response = remoteResponse.Result.Data ??
                new CommonResponseModel<ProductCategoryModel>();
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }

        return Task.FromResult(response);
    }
    public Task<CommonResponseModel<object>> Update(ProductCategoryModel request, string token = "") {
        var response = new CommonResponseModel<object>();

        try
        {
            var client = httpClientFactory.CreateClient();
            var headers = new Dictionary<string, string>();
            var path = string.Format("{0}{1}/{2}", clientConfig.Host, clientConfig.ProductCategory, request.Id);

            headers.Add("Authorization","Bearer " + token);
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
            var path = string.Format("{0}{1}/{2}", clientConfig.Host, clientConfig.ProductCategory, Id);

            headers.Add("Authorization","Bearer " + token);
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
