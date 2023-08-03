using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ApolloErp.Web.WebApi;
using Ae.ConsumerOrder.Service.Client.Request;

namespace Ae.ConsumerOrder.Service.Client.Clients
{
    public class StockClient : IStockClient
    {
        private readonly IHttpClientFactory clientFactory;
        private IConfiguration configuration { get; }

        public StockClient(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            this.clientFactory = clientFactory;
            this.configuration = configuration;
        }

        public async Task<ApiResult> SendOrderUseStock(SendOrderUseStockClientRequest request)
        {
            var client = clientFactory.CreateClient("StockServer");
            var response = await client.PostAsJsonAsync<SendOrderUseStockClientRequest, ApiResult>(configuration["StockServer:SendOrderUseStock"], request);
            return response;
        }

        public async Task<ApiResult> SendOrderReleaseStock(SendOrderReleaseStockClientRequest request)
        {
            var client = clientFactory.CreateClient("StockServer");
            var response = await client.PostAsJsonAsync<SendOrderReleaseStockClientRequest, ApiResult>(configuration["StockServer:SendOrderReleaseStock"], request);
            return response;
        }
    }
}
