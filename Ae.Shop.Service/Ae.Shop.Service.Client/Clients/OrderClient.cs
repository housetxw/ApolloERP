using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.Shop.Service.Client.Inteface;
using Ae.Shop.Service.Client.Model;
using Ae.Shop.Service.Client.Model.Order;
using Ae.Shop.Service.Client.Request;
using Ae.Shop.Service.Client.Response;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Client.Clients
{
    public class OrderClient : IOrderClient
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly ApolloErpLogger<OrderClient> _logger;
        private IConfiguration configuration { get; }

        public OrderClient(IHttpClientFactory clientFactory, IConfiguration configuration,
            ApolloErpLogger<OrderClient> logger
        )
        {
            this.clientFactory = clientFactory;
            this.configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// 根据门店ID批量获取订单数量
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<GetOrderCountByShopIdClientResponse>> BatchGetOrderCountByShopId( GetOrderCountByShopIdClientRequest request)
        {
            var client = clientFactory.CreateClient("OrderServer");
            ApiResult<List<GetOrderCountByShopIdClientResponse>> result = await client.
                PostAsJsonAsync<GetOrderCountByShopIdClientRequest, ApiResult<List<GetOrderCountByShopIdClientResponse>>>(configuration["OrderServer:BatchGetOrderCountByShopId"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                return null;
            }
        }

        public async  Task<List<OrderClientDTO>> GetOrderBaseInfo(GetOrderBaseInfoClientRequest request)
        {
            var client = clientFactory.CreateClient("OrderServer");
            ApiResult<List<OrderClientDTO>> result = await client.
                PostAsJsonAsync<GetOrderBaseInfoClientRequest, ApiResult<List<OrderClientDTO>>>(configuration["OrderServer:GetOrderBaseInfo"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                return null;
            }
        }

        public async Task<ApiResult<List<OrderProductDTO>>> GetOrderProduct(GetOrderProductRequest request)
        {
            var client = clientFactory.CreateClient("OrderServer");
            ApiResult<List<OrderProductDTO>> result = await client.
                PostAsJsonAsync<GetOrderProductRequest, ApiResult<List<OrderProductDTO>>>(configuration["OrderServer:GetOrderProduct"], request);
            return result;
        }
    }
}
