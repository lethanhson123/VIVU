using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIVU.Intergration.Implement
{
    public class CategoryService : ICategoriesService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ClientConfig clientConfig;

        public CategoryService(IHttpClientFactory httpClientFactory,
            IOptions<ClientConfig> clientConfig)
        {
            this.httpClientFactory = httpClientFactory;
            this.clientConfig = clientConfig.Value;
        }
        public Task<CommonResponseModel<CategoryModel>> Create(
            CategoryModel request, string token = "")
        {
            var response = new CommonResponseModel<CategoryModel>();

            try
            {
                var client = httpClientFactory.CreateClient();
                var headers = new Dictionary<string, string>();
                var path = string.Format("{0}{1}", clientConfig.Host, clientConfig.Categories);

                headers.Add("Authorization","Bearer "+ token);
                var remoteResponse = client.ExecutePost<CommonResponseModel<CategoryModel>>(
                  path, request, null, headers);
                response = remoteResponse.Result.Data ??
                    new CommonResponseModel<CategoryModel>();
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return Task.FromResult(response);
        }

        public Task<CommonResponseModel<object>> Delete(
            int Id, string token = "")
        {
            var response = new CommonResponseModel<object>();

            try
            {
                var client = httpClientFactory.CreateClient();
                var headers = new Dictionary<string, string>();

                headers.Add("Authorization", "Bearer " + token);
                var remoteResponse = client.ExecuteDelete<CommonResponseModel<object>>(
                    clientConfig.Host + clientConfig.Categories + "/" + Id, null, null, headers);
                response = remoteResponse.Result.Data ??
                    new CommonResponseModel<object>();
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return Task.FromResult(response);
        }

        public Task<CommonResponseModel<CategoryDetailModel>> Get(int Id)
        {
            var response = new CommonResponseModel<CategoryDetailModel>();

            try
            {
                var client = httpClientFactory.CreateClient();
                var headers = new Dictionary<string, string>();

                var remoteResponse = client
                    .ExecuteGet<CommonResponseModel<CategoryDetailModel>>(
                        clientConfig.Host + clientConfig.Categories + "/" + Id, null, headers);
                response = remoteResponse.Result.Data ??
                    new CommonResponseModel<CategoryDetailModel>();
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return Task.FromResult(response);
        }

        public Task<CommonResponseModel<IEnumerable<CategoryModel>>> GetAll(string keywords, string token = "")
        {
            var response = new CommonResponseModel<IEnumerable<CategoryModel>>();

            try
            {
                var client = httpClientFactory.CreateClient();
                var headers = new Dictionary<string, string>();
                var remoteResponse = client
                    .ExecuteGet<CommonResponseModel<IEnumerable<CategoryModel>>>(
                        clientConfig.Host + clientConfig.Categories, null, headers);
                response = remoteResponse.Result.Data ??
                    new CommonResponseModel<IEnumerable<CategoryModel>>();
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return Task.FromResult(response);
        }

        public Task<CommonResponseModel<IEnumerable<CategoryModel>>> GetWithQuery(CategoryQueryModel request, string token = "")
        {
            var response = new CommonResponseModel<IEnumerable<CategoryModel>>();

            try
            {
                var client = httpClientFactory.CreateClient();
                var headers = new Dictionary<string, string>();
                var remoteResponse = client.ExecuteGet<CommonResponseModel<IEnumerable<CategoryModel>>>(
                    clientConfig.Host + clientConfig.CategoriesGetWithQuery + "?" + request.ToQueryStringData());
                response = remoteResponse.Result.Data ??
                    new CommonResponseModel<IEnumerable<CategoryModel>>();
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return Task.FromResult(response);
        }

        public Task<CommonResponseModel<CategoryModel>> Update(CategoryModel request, string token = "")
        {
            var response = new CommonResponseModel<CategoryModel>();

            try
            {
                var client = httpClientFactory.CreateClient();
                var headers = new Dictionary<string, string>();

                headers.Add("Authorization", "Bearer " + token);
                var remoteResponse = client.ExecutePut<CommonResponseModel<CategoryModel>>(
                    clientConfig.Host + clientConfig.Categories + "/" + request.Id, request, null, headers);
                response = remoteResponse.Result.Data ??
                    new CommonResponseModel<CategoryModel>();
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return Task.FromResult(response);
        }
    }
}
