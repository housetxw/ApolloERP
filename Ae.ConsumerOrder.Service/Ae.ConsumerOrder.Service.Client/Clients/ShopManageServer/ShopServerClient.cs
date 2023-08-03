using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ApolloErp.Web.WebApi;
using Ae.ConsumerOrder.Service.Client.Request;
using Ae.ConsumerOrder.Service.Client.Response;

namespace Ae.ConsumerOrder.Service.Client.Clients
{
    public class ShopServerClient : IShopServerClient
    {
        private readonly IHttpClientFactory clientFactory;
        private IConfiguration configuration { get; }

        public ShopServerClient(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            this.clientFactory = clientFactory;
            this.configuration = configuration;
        }

        public async Task<ApiResult<List<GetShopServiceListWithPIDResponse>>> GetShopServiceListWithPID(GetShopServiceListWithPIDRequest request)
        {
            var client = clientFactory.CreateClient("ShopManageServer");
            var response = await client.PostAsJsonAsync<GetShopServiceListWithPIDRequest, ApiResult<List<GetShopServiceListWithPIDResponse>>>(configuration["ShopManageServer:GetShopServiceListWithPID"], request);
            return response;
        }
    }
}
