using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ApolloErp.Web.WebApi;
using Ae.ConsumerOrder.Service.Core.Model;
using Ae.ConsumerOrder.Service.Core.Request.OrderQuery;

namespace Ae.ConsumerOrder.Service.Client.Clients.OrderServer
{
    public class OrderQueryClient : IOrderQueryClient
    {
        private readonly IHttpClientFactory _clientFactory;
        private IConfiguration _configuration { get; }

        public OrderQueryClient(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
           _clientFactory = clientFactory;
            _configuration = configuration;
        }
        public async Task<ApiResult<List<OrderDTO>>> GetOrderBaseInfo(GetOrderBaseInfoClientRequest request)
        {
            var client = _clientFactory.CreateClient("OrderServer");

            var response =
                await client
                    .PostAsJsonAsync<GetOrderBaseInfoClientRequest, ApiResult<List<OrderDTO>>>(
                        _configuration["OrderServer:GetOrderBaseInfo"], request);

            return response;
        }
    }
}
