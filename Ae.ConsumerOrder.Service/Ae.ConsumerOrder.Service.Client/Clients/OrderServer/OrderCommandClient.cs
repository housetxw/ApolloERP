using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ApolloErp.Web.WebApi;
using Ae.ConsumerOrder.Service.Client.Request;
using Ae.ConsumerOrder.Service.Core.Request;
using Ae.ConsumerOrder.Service.Core.Request.OrderCommand;

namespace Ae.ConsumerOrder.Service.Client.Clients
{
    public class OrderCommandClient : IOrderCommandClient
    {
        private readonly IHttpClientFactory clientFactory;
        private IConfiguration configuration { get; }

        public OrderCommandClient(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            this.clientFactory = clientFactory;
            this.configuration = configuration;
        }

        public async Task<ApiResult> SyncOrder(SyncOrderRequest request)
        {
            var client = clientFactory.CreateClient("OrderServer");
            var response = await client.PostAsJsonAsync<SyncOrderRequest, ApiResult>(configuration["OrderServer:SyncOrder"], request);
            return response;
        }

        public async Task<ApiResult<long>> UpdateOrderStatus(UpdateOrderStatusRequest request)
        {
            var client = clientFactory.CreateClient("OrderServer");
            var response = await client.PostAsJsonAsync<UpdateOrderStatusRequest, ApiResult<long>>(
                configuration["OrderServer:UpdateOrderStatus"], request);
            return response;
        }

        public async Task<ApiResult> UpdateOrderMainStatus(UpdateOrderMainStatusRequest request)
        {
            var client = clientFactory.CreateClient("OrderServer");
            var response = await client.PostAsJsonAsync<UpdateOrderMainStatusRequest, ApiResult>(configuration["OrderServer:UpdateOrderMainStatus"], request);
            return response;
        }

        public async Task<ApiResult> UpdateOrderStockStatus(UpdateOrderStockStatusRequest request)
        {
            var client = clientFactory.CreateClient("OrderServer");
            var response = await client.PostAsJsonAsync<UpdateOrderStockStatusRequest, ApiResult>(configuration["OrderServer:UpdateOrderStockStatus"], request);
            return response;
        }

        public async Task<ApiResult> UpdateProductStockStatus(UpdateProductStockStatusRequest request)
        {
            var client = clientFactory.CreateClient("OrderServer");
            var response = await client.PostAsJsonAsync<UpdateProductStockStatusRequest, ApiResult>(configuration["OrderServer:UpdateProductStockStatus"], request);
            return response;
        }

        public async Task<ApiResult> UpdatePayStatus(UpdatePayStatusRequest request)
        {
            var client = clientFactory.CreateClient("OrderServer");
            var response = await client.PostAsJsonAsync<UpdatePayStatusRequest, ApiResult>(configuration["OrderServer:UpdatePayStatus"], request);
            return response;
        }

        public async Task<ApiResult> UpdateMoneyArriveStatus(UpdateMoneyArriveStatusRequest request)
        {
            var client = clientFactory.CreateClient("OrderServer");
            var response = await client.PostAsJsonAsync<UpdateMoneyArriveStatusRequest, ApiResult>(configuration["OrderServer:UpdateMoneyArriveStatus"], request);
            return response;
        }

        public async Task<ApiResult> UpdateProductTotalCostPrice(UpdateProductTotalCostPriceRequest request)
        {
            var client = clientFactory.CreateClient("OrderServer");
            var response = await client.PostAsJsonAsync<UpdateProductTotalCostPriceRequest, ApiResult>(configuration["OrderServer:UpdateProductTotalCostPrice"], request);
            return response;
        }

        public async Task<ApiResult> OrderReverseNotify(OrderReverseNotifyRequest request)
        {
            var client = clientFactory.CreateClient("OrderServer");
            var response = await client.PostAsJsonAsync<OrderReverseNotifyRequest, ApiResult>(configuration["OrderServer:OrderReverseNotify"], request);
            return response;
        }
        /// <summary>
        /// 修改订单预约状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> UpdateOrderReserveStatus(UpdateOrderReserveStatusClientRequest request)
        {
            var client = clientFactory.CreateClient("OrderServer");
            var response = await client.PostAsJsonAsync<UpdateOrderReserveStatusClientRequest, ApiResult>(configuration["OrderServer:UpdateOrderReserveStatus"], request);
            return response;
        }
    }
}
