using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIVU.Intergration.Implement
{
    public class BlogService : IBlogService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ClientConfig clientConfig;

        public BlogService(IHttpClientFactory httpClientFactory,
            IOptions<ClientConfig> clientConfig)
        {
            this.httpClientFactory = httpClientFactory;
            this.clientConfig = clientConfig.Value;
        }

        public Task<CommonResponseModel<BlogModel>> Create(
            BlogDetailModel request, string token = "")
        {
            var response = new CommonResponseModel<BlogModel>();

            try
            {
                var client = httpClientFactory.CreateClient();
                var headers = new Dictionary<string, string>();
                var path = string.Format("{0}{1}", clientConfig.Host, clientConfig.Blog);

                headers.Add("Authorization", "Bearer " + token);
                var remoteResponse = client.ExecutePost<CommonResponseModel<BlogModel>>(
                    path, request, null, headers);
                response = remoteResponse.Result.Data ?? new CommonResponseModel<BlogModel>();
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
                var path = string.Format("{0}{1}/{2}", clientConfig.Host, clientConfig.Blog,Id);

                headers.Add("Authorization", "Bearer " + token);
                var remoteResponse = client.ExecuteDelete<CommonResponseModel<object>>(
                   path, null, null, headers);
                response = remoteResponse.Result.Data ?? new CommonResponseModel<object>();
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return Task.FromResult(response);
        }

        public Task<CommonResponseModel<IEnumerable<BlogModel>>> GetWithQuery(
            BlogQueryModel request, string token = "")
        {
            var response = new CommonResponseModel<IEnumerable<BlogModel>>();

            try
            {
                var client = httpClientFactory.CreateClient();
                var headers = new Dictionary<string, string>();
                var path = string.Format("{0}{1}?{2}", clientConfig.Host, clientConfig.Blog, request.ToQueryStringData());

                headers.Add("Authorization", "Bearer " + token);

                var remoteResponse = client
                    .ExecuteGet<CommonResponseModel<IEnumerable<BlogModel>>>(
                        path, null, headers);
                response = remoteResponse.Result.Data ??
                    new CommonResponseModel<IEnumerable<BlogModel>>();
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return Task.FromResult(response);
        }

        public Task<CommonResponseModel<BlogDetailModel>> Get(int Id)
        {
            var response = new CommonResponseModel<BlogDetailModel>();

            try
            {
                var client = httpClientFactory.CreateClient();
                var headers = new Dictionary<string, string>();
                var path = string.Format("{0}{1}/{2}", clientConfig.Host, clientConfig.Blog, Id);
                var remoteResponse = client
                    .ExecuteGet<CommonResponseModel<BlogDetailModel>>(
                        path, null, headers);
                response = remoteResponse.Result.Data ??
                    new CommonResponseModel<BlogDetailModel>();
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return Task.FromResult(response);
        }

        public Task<CommonResponseModel<IEnumerable<BlogModel>>> Get(string keywords, string token = "")
        {
            var response = new CommonResponseModel<IEnumerable<BlogModel>>();

            try
            {
                var client = httpClientFactory.CreateClient();
                var headers = new Dictionary<string, string>();
                var url = clientConfig.Host + clientConfig.Blog;

                if (!string.IsNullOrEmpty(keywords))
                {
                    url += "?keywords=" + keywords;
                }

                headers.Add("Authorization",token);
                var remoteResponse = client.ExecuteGet<CommonResponseModel<IEnumerable<BlogModel>>>(
                   url, null, headers);
                response = remoteResponse.Result.Data ??
                    new CommonResponseModel<IEnumerable<BlogModel>>();
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return Task.FromResult(response);
        }

        public Task<CommonResponseModel<BlogModel>> Update(
            BlogDetailModel request, string token = "")
        {
            var response = new CommonResponseModel<BlogModel>();

            try
            {
                var client = httpClientFactory.CreateClient();
                var headers = new Dictionary<string, string>();

                headers.Add("Authorization","Bearer " + token);
                var remoteResponse = client.ExecutePut<CommonResponseModel<BlogModel>>(
                    clientConfig.Host + clientConfig.Blog + "/" + request.Id, request, null, headers);
                response = remoteResponse.Result.Data ??
                    new CommonResponseModel<BlogModel>();
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return Task.FromResult(response);
        }
    }
}
