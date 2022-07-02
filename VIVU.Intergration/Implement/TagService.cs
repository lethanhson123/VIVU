using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIVU.Intergration.Implement
{
    public class TagService : ITagService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ClientConfig clientConfig;

        public TagService(IHttpClientFactory httpClientFactory,
            IOptions<ClientConfig> clientConfig)
        {
            this.httpClientFactory = httpClientFactory;
            this.clientConfig = clientConfig.Value;
        }

        public Task<CommonResponseModel<TagModel>> Create(TagModel request, string token = "")
        {
            var response = new CommonResponseModel<TagModel>();

            try
            {
                var client = httpClientFactory.CreateClient();
                var headers = new Dictionary<string, string>();

                headers.Add("Authorization", "Bearer " +  token);
                var remoteResponse = client.ExecutePost<CommonResponseModel<TagModel>>(
                    clientConfig.Host + clientConfig.Tag, request, null, headers);
                response = remoteResponse.Result.Data ?? new CommonResponseModel<TagModel>();
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return Task.FromResult(response);
        }

        public Task<CommonResponseModel<object>> Delete(int Id, string token = "")
        {
            var response = new CommonResponseModel<object>();

            try
            {
                var client = httpClientFactory.CreateClient();
                var headers = new Dictionary<string, string>();

                headers.Add("Authorization", "Bearer " + token);
                var remoteResponse = client.ExecuteDelete<CommonResponseModel<object>>(
                    clientConfig.Host + clientConfig.Tag + "/" + Id, null, null, headers);
                response = remoteResponse.Result.Data ?? new CommonResponseModel<object>();
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return Task.FromResult(response);
        }

        public Task<CommonResponseModel<IEnumerable<TagModel>>> GetWithQuery(TagQueryModel request, string token = "")
        {
            var response = new CommonResponseModel<IEnumerable<TagModel>>();

            try
            {
                var client = httpClientFactory.CreateClient();
                var headers = new Dictionary<string, string>();

                var remoteResponse = client.ExecuteGet<CommonResponseModel<IEnumerable<TagModel>>>(
                    clientConfig.Host + clientConfig.TagGetWithQuery + "?" + request.ToQueryStringData(), null, headers);
                response = remoteResponse.Result.Data ?? new CommonResponseModel<IEnumerable<TagModel>>();
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return Task.FromResult(response);
        }

        public Task<CommonResponseModel<TagDetailModel>> Get(int Id)
        {
            var response = new CommonResponseModel<TagDetailModel>();

            try
            {
                var client = httpClientFactory.CreateClient();
                var headers = new Dictionary<string, string>();
                var remoteResponse = client.ExecuteGet<CommonResponseModel<TagDetailModel>>(
                    clientConfig.Host + clientConfig.Tag + "/" + Id, null, headers);
                response = remoteResponse.Result.Data ?? new CommonResponseModel<TagDetailModel>();
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return Task.FromResult(response);
        }

        public Task<CommonResponseModel<TagModel>> Update(TagModel request, string token = "")
        {
            var response = new CommonResponseModel<TagModel>();

            try
            {
                var client = httpClientFactory.CreateClient();
                var headers = new Dictionary<string, string>();

                headers.Add("Authorization","Bearer " +token);
                var remoteResponse = client.ExecutePut<CommonResponseModel<TagModel>>(
                    clientConfig.Host + clientConfig.Tag + "/" + request.Id, request, null, headers);
                response = remoteResponse.Result.Data ?? new CommonResponseModel<TagModel>();
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return Task.FromResult(response);
        }

        public Task<CommonResponseModel<IEnumerable<TagModel>>> GetAll(string keywords, string token = "")
        {
            var response = new CommonResponseModel<IEnumerable<TagModel>>();

            try
            {
                var client = httpClientFactory.CreateClient();
                var headers = new Dictionary<string, string>();

                var url = clientConfig.Host + clientConfig.Tag;

                if (!string.IsNullOrEmpty(keywords))
                {
                    url += "?keywords=" + keywords;
                }
                var remoteResponse = client.ExecuteGet<CommonResponseModel<IEnumerable<TagModel>>>(
                    url, null, headers);
                response = remoteResponse.Result.Data ?? new CommonResponseModel<IEnumerable<TagModel>>();
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return Task.FromResult(response);
        }
    }
}
