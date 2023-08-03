using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ApolloErp.Web.WebApi;
using Ae.ShopOrder.Service.Core.Request.Stock;

namespace Ae.ShopOrder.Service.Client.Clients.Stock.Impl
{
    /// <summary>
    /// 库存Client
    /// </summary>
    public class RgStockClient : IRgStockClient
    {
        private readonly IHttpClientFactory _clientFactory;
        private IConfiguration _configuration { get; }

        public RgStockClient(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
        }
        public async Task<ApiResult> UpdateOrderInstallReleaseStock(UpdateOrderInstallReleaseStockRequest request)
        {
            var client = _clientFactory.CreateClient("StockServer");

            var response =
                await client
                    .PostAsJsonAsync<UpdateOrderInstallReleaseStockRequest, ApiResult>(
                        _configuration["StockServer:UpdateOrderInstallReleaseStockRequest"], request);

            return response;
        }
    }
}
