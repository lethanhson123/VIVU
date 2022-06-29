namespace VIVU.Intergration.Implement;

public class UserService : IUserService
{
    private readonly IHttpClientFactory httpClientFactory;
    private readonly ClientConfig clientConfig;

    public UserService(IHttpClientFactory httpClientFactory,
        IOptions<ClientConfig> clientConfig)
    {
        this.httpClientFactory = httpClientFactory;
        this.clientConfig = clientConfig.Value;
    }

    public Task<CommonResponseModel<UserLoginDataTransferObject>> Login(LoginRequest request)
    {
        var response = new CommonResponseModel<UserLoginDataTransferObject>();

        try
        {
            var client = httpClientFactory.CreateClient();
            var path = string.Format("{0}{1}", clientConfig.Host, clientConfig.Login);

            var remoteResponse = client.ExecutePost<CommonResponseModel<UserLoginDataTransferObject>>(
                clientConfig.Host + clientConfig.Login, request);
            response = remoteResponse.Result.Data ?? 
                new CommonResponseModel<UserLoginDataTransferObject>();
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }

        return Task.FromResult(response);
    }
}
