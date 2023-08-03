using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ApolloErp.Web.WebApi;
using Ae.ShopOrder.Service.Client.Model.OrderForC;
using Ae.ShopOrder.Service.Client.Request.OrderForC;

namespace Ae.ShopOrder.Service.Client.Clients.OrderForC.Impl
{
    public class OrderQueryForCClient:IOrderQueryForCClient
    {
        private readonly IHttpClientFactory _clientFactory;
        private IConfiguration _configuration { get; }

        public OrderQueryForCClient(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
        }
        /// <summary>
        /// 得到车型信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<OrderCarClientDTO>>> GetOrderCar(GetOrderCarClientRequest request)
        {
            var client = _clientFactory.CreateClient("CustomerOrderServer");

            var response =
                await client
                    .PostAsJsonAsync<GetOrderCarClientRequest, ApiResult< List < OrderCarClientDTO >>> (
                        _configuration["CustomerOrderServer:GetOrderCar"], request);

            return response;
        }
    }
}
