using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Interface;
using Ae.C.MiniApp.Api.Client.Model.Activity;
using Ae.C.MiniApp.Api.Client.Request;
using Ae.C.MiniApp.Api.Client.Request.Activity;
using Ae.C.MiniApp.Api.Client.Response.Activity;

namespace Ae.C.MiniApp.Api.Client.Clients.Activity
{
    public class ActivityClient : IActivityClient
    {
        private readonly IHttpClientFactory clientFactory;
        private IConfiguration configuration { get; }
        public ActivityClient(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            this.clientFactory = clientFactory;
            this.configuration = configuration;
        }

        public async Task<ApiResult<GenMinAppCodeResponse>> GenMinAppCode(GenMinAppCodeClientRequest request)
        {
            var client = clientFactory.CreateClient("ActivityServer");
            var response = await client.PostAsJsonAsync<GenMinAppCodeClientRequest, ApiResult<GenMinAppCodeResponse>>(configuration["ActivityServer:GenMinAppCode"], request);
            return response;
        }

        public async Task<ApiResult<PromoteContentDTO>> GetPromoteContent(GetPromoteContentRequest request)
        {
            var client = clientFactory.CreateClient("ActivityServer");
            var response = await client.GetAsJsonAsync<GetPromoteContentRequest, ApiResult<PromoteContentDTO>>(configuration["ActivityServer:GetPromoteContent"], request);
            return response;
        }

        public async Task<ApiResult<object>> GetWxacodeScene(GetWxacodeSceneClientRequest request)
        {
            var client = clientFactory.CreateClient("ActivityServer");
            var response = await client.GetAsJsonAsync<GetWxacodeSceneClientRequest, ApiResult<object>>(configuration["ActivityServer:GetWxacodeScene"], request);
            return response;
        }

        public async Task<ApiResult> SharePromotionContent(ApiRequest<SharePromotionContentClientRequest> request)
        {
            var client = clientFactory.CreateClient("ActivityServer");
            var response = await client.PostAsJsonAsync<ApiRequest<SharePromotionContentClientRequest>, ApiResult>(configuration["ActivityServer:SharePromotionContent"], request);
            return response;
        }
    }
}
