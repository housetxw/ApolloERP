using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Interface;
using Ae.C.MiniApp.Api.Client.Request.Order;
using Ae.C.MiniApp.Api.Client.Response.Order;
using Ae.C.MiniApp.Api.Core.Request.Reverse;

namespace Ae.C.MiniApp.Api.Client.Clients.Order
{
    public class ShopOrderQueryClient : IShopOrderQueryClient
    {
        private readonly IHttpClientFactory clientFactory;
        private IConfiguration configuration { get; }

        public ShopOrderQueryClient(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            this.clientFactory = clientFactory;
            this.configuration = configuration;
        }

        public async Task<ApiResult<GetOrderDetailClientResponse>> GetOrderDetailForMiniApp(GetOrderDetailClientRequest request)
        {
            var client = clientFactory.CreateClient("ShopOrderServer");
            var response = await client.GetAsJsonAsync<GetOrderDetailClientRequest, ApiResult<GetOrderDetailClientResponse>>(configuration["ShopOrderServer:GetOrderDetailForMiniApp"], request);
            return response;
        }

        public async Task<ApiResult> CancelOrder(ApiRequest<CancelNewOrderRequest> request)
        {
            var client = clientFactory.CreateClient("ShopOrderServer");
            var response = await client.PostAsJsonAsync<ApiRequest<CancelNewOrderRequest>, ApiResult>(configuration["ShopOrderServer:CancelOrder"], request);
            return response;
        }
    }
}
