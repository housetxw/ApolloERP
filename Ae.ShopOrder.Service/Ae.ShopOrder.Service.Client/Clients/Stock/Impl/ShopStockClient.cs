using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ApolloErp.Web.WebApi;
using Ae.ShopOrder.Service.Core.Model.Stock;
using Ae.ShopOrder.Service.Core.Request.Stock;

namespace Ae.ShopOrder.Service.Client.Clients.Stock.Impl
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
        public async Task<ApiResult> OrderInstallReduceStock(OrderInstallReduceStockRequest request)
        {
            var client = _clientFactory.CreateClient("ShopStockServer");

            var response =
                await client
                    .PostAsJsonAsync<OrderInstallReduceStockRequest, ApiResult>(
                        _configuration["ShopStockServer:OrderInstallReduceStock"], request);

            return response;
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

        public async Task<ApiResult> ReleaseStock(ReleaseStockRequest request)
        {
            var client = _clientFactory.CreateClient("ShopStockServer");

            var response =
                await client
                    .PostAsJsonAsync<ReleaseStockRequest, ApiResult>(
                        _configuration["ShopStockServer:ReleaseStock"], request);

            return response;
        }

        public async Task<ApiResult> OrderCancelReleaseStockNoBatch(ReleaseStockRequest request)
        {
            var client = _clientFactory.CreateClient("ShopStockServer");

            var response =
                await client
                    .PostAsJsonAsync<ReleaseStockRequest, ApiResult>(
                        _configuration["ShopStockServer:OrderCancelReleaseStockNoBatch"], request);

            return response;
        }

        public async Task<ApiResult<List<ProductStockDTO>>> GetTransferProductStocks(GetShopProductStocksRequest request)
        {
            var client = _clientFactory.CreateClient("ShopStockServer");

            var response =
                await client
                    .PostAsJsonAsync<GetShopProductStocksRequest, ApiResult<List<ProductStockDTO>>>(
                        _configuration["ShopStockServer:GetTransferProductStocks"], request);

            return response;
        }
    }
}
