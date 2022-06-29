namespace VIVU.Intergration.Implement;

internal class SalesOrderService : ISalesOrderService
{
    private readonly IHttpClientFactory httpClientFactory;
    private readonly ClientConfig clientConfig;

    public SalesOrderService(IHttpClientFactory httpClientFactory,
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
    public Task<CommonResponseModel<IEnumerable<SalesOrderDetailModelResponse>>> Get(string token = "")
    {
        var response = new CommonResponseModel<IEnumerable<SalesOrderDetailModelResponse>>();
        try
        {
            var client = httpClientFactory.CreateClient();
            var headers = new Dictionary<string, string>();
            var path = string.Format("{0}{1}", clientConfig.Host, clientConfig.SaleOrder);
            headers.Add("Authorization", token);
            var remoteResponse = client
                .ExecuteGet<CommonResponseModel<IEnumerable<SalesOrderDetailModelResponse>>>(
                   path, null, headers);
            response = remoteResponse.Result.Data ??
                new CommonResponseModel<IEnumerable<SalesOrderDetailModelResponse>>();
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
    public Task<CommonResponseModel<IEnumerable<SalesOrderDetailModelResponse>>> Get(SalesOrderQueryModel request, string token = "")
    {

        var response = new CommonResponseModel<IEnumerable<SalesOrderDetailModelResponse>>();
        try
        {
            var client = httpClientFactory.CreateClient();
            var headers = new Dictionary<string, string>();
            var path = string.Format("{0}{1}?{2}", clientConfig.Host, clientConfig.SaleOrderGetWithQuery, request.ToQueryStringData());

            headers.Add("Authorization", token);
            var remoteResponse = client
                .ExecuteGet<CommonResponseModel<IEnumerable<SalesOrderDetailModelResponse>>>(
                   path, null, headers);
            response = remoteResponse.Result.Data ??
                new CommonResponseModel<IEnumerable<SalesOrderDetailModelResponse>>();
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }
        return Task.FromResult(response);
    }

    public Task<CommonResponseModel<SalesOrderModelResponse>> GetDetail(string id, string token)
    {
        var response = new CommonResponseModel<SalesOrderModelResponse>();
        try
        {
            var client = httpClientFactory.CreateClient();
            var headers = new Dictionary<string, string>();
            var path = string.Format("{0}{1}/{2}", clientConfig.Host, clientConfig.SaleOrder, id);

            headers.Add("Authorization", token);
            var remoteResponse = client
                .ExecuteGet<CommonResponseModel<SalesOrderModelResponse>>(
                   path, null, headers);
            response = remoteResponse.Result.Data ??
                new CommonResponseModel<SalesOrderModelResponse>();
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }
        return Task.FromResult(response);
    }
    public Task<CommonResponseModel<SalesOrderModel>> Create(SalesOrderModel request, string token = "")
    {
        var response = new CommonResponseModel<SalesOrderModel>();

        try
        {
            var client = httpClientFactory.CreateClient();
            var headers = new Dictionary<string, string>();
            var path = string.Format("{0}{1}", clientConfig.Host, clientConfig.SaleOrder);

            headers.Add("Authorization", token);
            var remoteResponse = client.ExecutePost<CommonResponseModel<SalesOrderModel>>(
                path, request, null, headers);
            response = remoteResponse.Result.Data ??
                new CommonResponseModel<SalesOrderModel>();
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }

        return Task.FromResult(response);
    }
    public Task<CommonResponseModel<object>> Update(SalesOrderModel request, string token = "")
    {
        var response = new CommonResponseModel<object>();

        try
        {
            var client = httpClientFactory.CreateClient();
            var headers = new Dictionary<string, string>();
            var path = string.Format("{0}{1}/{2}", clientConfig.Host, clientConfig.SaleOrder, request.Id);

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
    public Task<CommonResponseModel<object>> Status(string id, int status, string token = "")
    {
        var response = new CommonResponseModel<object>();

        try
        {
            var client = httpClientFactory.CreateClient();
            var headers = new Dictionary<string, string>();
            var path = string.Format("{0}{1}/{2}", clientConfig.Host, clientConfig.SaleOrderStatus, id);
            headers.Add("Authorization", token);
            var remoteResponse = client.ExecutePut<CommonResponseModel<object>>(
               path, status, null, headers);
            response = remoteResponse.Result.Data ??
                new CommonResponseModel<object>();
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }

        return Task.FromResult(response);
    }

    public Task<CommonResponseModel<object>> Delete(string Id, string token = "")
    {
        var response = new CommonResponseModel<object>();

        try
        {
            var client = httpClientFactory.CreateClient();
            var headers = new Dictionary<string, string>();
            var path = string.Format("{0}{1}/{2}", clientConfig.Host, clientConfig.SaleOrder, Id);

            headers.Add("Authorization", token);
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
