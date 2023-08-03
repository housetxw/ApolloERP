using Ae.C.MiniApp.Api.Client.Request;
using Ae.C.MiniApp.Api.Client.Response;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Ae.C.MiniApp.Api.Client.Interface;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Request.Order;
using Ae.C.MiniApp.Api.Client.Response.Order;

namespace Ae.C.MiniApp.Api.Client.Clients.Order
{
    public class ConsumerOrderCommandClient : IConsumerOrderCommandClient
    {
        private readonly IHttpClientFactory clientFactory;
        private IConfiguration configuration { get; }
        public ConsumerOrderCommandClient(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            this.clientFactory = clientFactory;
            this.configuration = configuration;
        }

        public async Task<ApiResult<SubmitOrderClientResponse>> SubmitOrder(ApiRequest<SubmitOrderClientRequest> request)
        {
            var client = clientFactory.CreateClient("ConsumerOrderServer");
            var response = await client.PostAsJsonAsync<ApiRequest<SubmitOrderClientRequest>, ApiResult<SubmitOrderClientResponse>>(configuration["ConsumerOrderServer:SubmitOrder"], request);
            return response;
        }

        public async Task<ApiResult<BuyAgainClientResponse>> BuyAgain(ApiRequest<BuyAgainClientRequest> request)
        {
            var client = clientFactory.CreateClient("ConsumerOrderServer");
            var response = await client.PostAsJsonAsync<ApiRequest<BuyAgainClientRequest>, ApiResult<BuyAgainClientResponse>>(configuration["ConsumerOrderServer:BuyAgain"], request);
            return response;
        }
    }
}
