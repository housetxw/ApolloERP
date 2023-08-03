using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.Order.Service.Client.Interface;
using Ae.Order.Service.Core.Model.Order;
using Ae.Order.Service.Core.Request.Order;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Order.Service.Client.Clients.ShopOrder
{
    public class ShopOrderClient : IShopOrderClient
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly ApolloErpLogger<ShopOrderClient> logger;
        private readonly IConfiguration configuration;
        private readonly HttpClient client;

        public ShopOrderClient(IHttpClientFactory clientFactory, ApolloErpLogger<ShopOrderClient> logger, IConfiguration configuration)
        {
            this.clientFactory = clientFactory;
            client = clientFactory.CreateClient("ShopOrderServer");
            this.configuration = configuration;
            this.logger = logger;
        }

        public async Task<ApiResult<List<OrderCarDTO>>> GetOrderCarsInfo(GetOrderCarsRequest request)
        {
            var res = await client.PostAsJsonAsync<GetOrderCarsRequest, ApiResult<List<OrderCarDTO>>>(configuration["ShopOrderServer:GetOrderCarInfo"], request);
            return res;
        }
    }
}
