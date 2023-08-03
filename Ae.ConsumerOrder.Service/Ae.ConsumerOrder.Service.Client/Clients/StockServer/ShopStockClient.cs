using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ApolloErp.Web.WebApi;
using Ae.ConsumerOrder.Service.Client.Request.Stock;

namespace Ae.ConsumerOrder.Service.Client.Clients.StockServer
{
    public class ShopStockClient : IShopStockClient
    {
        private readonly IHttpClientFactory _clientFactory;
        private IConfiguration _configuration { get; }

        public ShopStockClient(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
        }

        public async Task<ApiResult> OrderOccupyStock(OrderOccupyStockRequest request)
        {
            var client = _clientFactory.CreateClient("ShopStockServer");

            var response =
                await client
                    .PostAsJsonAsync<OrderOccupyStockRequest, ApiResult>(
                        _configuration["ShopStockServer:OrderOccupyStock"], request);

            return response;
        }
    }
}
