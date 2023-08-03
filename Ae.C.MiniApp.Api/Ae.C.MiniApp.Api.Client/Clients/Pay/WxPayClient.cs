using Microsoft.Extensions.Configuration;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Clients.Interface;
using Ae.C.MiniApp.Api.Client.Request.Pay;
using Ae.C.MiniApp.Api.Client.Response.Pay;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ae.C.MiniApp.Api.Client.Clients.Pay
{
    public class WxPayClient : IWxPayClient
    {
        private readonly IHttpClientFactory clientFactory;
        private IConfiguration configuration { get; }
        public WxPayClient(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            this.clientFactory = clientFactory;
            this.configuration = configuration;
        }

        public async Task<ApiResult<CreateWxPrePayOrderForMiniAppResponse>> CreateWxPrePayOrderForMiniApp(CreateWxPrePayOrderForMiniAppRequest request)
        {
            var client = clientFactory.CreateClient("PayServer");
            var response = await client.PostAsJsonAsync<CreateWxPrePayOrderForMiniAppRequest, ApiResult<CreateWxPrePayOrderForMiniAppResponse>>(configuration["PayServer:CreateWxPrePayOrderForMiniApp"], request);
            return response;
        }

        public async Task<string> WxPayResultNotify(string requestXml)
        {
            var client = clientFactory.CreateClient("PayServer");
            var uri = configuration["PayServer:WxPayResultNotify"];
            var responseXml = await client.PostAsStringAsync(uri, requestXml);
            return responseXml;
           
        }
    }
}
