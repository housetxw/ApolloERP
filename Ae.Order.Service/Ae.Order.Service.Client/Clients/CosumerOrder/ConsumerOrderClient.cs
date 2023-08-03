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

namespace Ae.Order.Service.Client.Clients.CosumerOrder
{
    public class ConsumerOrderClient : IConsumerOrderClient
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly ApolloErpLogger<ConsumerOrderClient> logger;
        private readonly IConfiguration configuration;
        private readonly HttpClient client;


        public ConsumerOrderClient(IHttpClientFactory clientFactory, ApolloErpLogger<ConsumerOrderClient> logger, IConfiguration configuration)
        {
            this.clientFactory = clientFactory;
            client = clientFactory.CreateClient("ConsumerOrderServer");
            this.configuration = configuration;
            this.logger = logger;
        }

        public async Task<ApiResult> CancelOrder(ApiRequest<CancelOrderRequest> request)
        {
            var res = await client.PostAsJsonAsync<ApiRequest<CancelOrderRequest>, ApiResult>(configuration["ConsumerOrderServer:CancelOrder"], request);
            return res;
        }

        public async Task<ApiResult<List<OrderCarDTO>>> GetOrderCarsInfo(GetOrderCarsRequest request)
        {
            var res = await client.PostAsJsonAsync<GetOrderCarsRequest, ApiResult<List<OrderCarDTO>>>(configuration["ConsumerOrderServer:GetOrderCarInfo"], request);
            return res;
        }

        public async Task<ApiResult> UpdateOrderDispatchStatus(UpdateOrderDispatchStatusRequest request)
        {
            var res = await client.PostAsJsonAsync<UpdateOrderDispatchStatusRequest, ApiResult>(configuration["ConsumerOrderServer:UpdateOrderDispatchStatus"], request);
            return res;
        }

        public async Task<ApiResult> UpdatePayStatus(UpdatePayStatusRequest request)
        {
            var res = await client.PostAsJsonAsync<UpdatePayStatusRequest, ApiResult>(configuration["ConsumerOrderServer:UpdatePayStatus"], request);
            return res;
        }
    }
}
