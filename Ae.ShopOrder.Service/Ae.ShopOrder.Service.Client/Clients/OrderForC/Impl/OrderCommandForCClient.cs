using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ApolloErp.Web.WebApi;
using Ae.ShopOrder.Service.Core.Request.Order;
using Ae.ShopOrder.Service.Core.Request.OrderForC;

namespace Ae.ShopOrder.Service.Client.Clients.OrderForC.Impl
{
    /// <summary>
    /// 订单操作ForC
    /// </summary>
    public class OrderCommandForCClient : IOrderCommandForCClient
    {
        private readonly IHttpClientFactory _clientFactory;
        private IConfiguration _configuration { get; }

        public OrderCommandForCClient(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
        }
        /// <summary>
        /// 更新订单状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<long>> UpdateOrderStatus(UpdateOrderStatusRequest request)
        {
            var client = _clientFactory.CreateClient("CustomerOrderServer");

            var response =
                await client
                    .PostAsJsonAsync<UpdateOrderStatusRequest, ApiResult<long>>(
                        _configuration["CustomerOrderServer:UpdateOrderStatus"], request);

            return response;
        }

        public async Task<ApiResult> UpdateOrderDispatchStatus(UpdateOrderDispatchStatusRequest request)
        {
            var client = _clientFactory.CreateClient("CustomerOrderServer");

            var response =
                await client
                    .PostAsJsonAsync<UpdateOrderDispatchStatusRequest, ApiResult>(
                        _configuration["CustomerOrderServer:UpdateOrderDispatchStatus"], request);

            return response;
        }

        public async Task<ApiResult> CancelOrder(ApiRequest<CancelOrderRequest> request)
        {
            var client = _clientFactory.CreateClient("CustomerOrderServer");

            var response =
                await client
                    .PostAsJsonAsync<ApiRequest<CancelOrderRequest>, ApiResult>(
                        _configuration["CustomerOrderServer:CancelOrder"], request);

            return response;
        }

        public async Task<ApiResult> UpdatePayStatus(UpdatePayStatusRequest request)
        {
            var client = _clientFactory.CreateClient("CustomerOrderServer");

            var response =
                await client
                    .PostAsJsonAsync<UpdatePayStatusRequest, ApiResult>(
                        _configuration["CustomerOrderServer:UpdatePayStatus"], request);

            return response;
        }

        public async Task<ApiResult> UpdateMoneyArriveStatus(UpdateMoneyArriveStatusRequest request)
        {
            var client = _clientFactory.CreateClient("CustomerOrderServer");

            var response =
                await client
                    .PostAsJsonAsync<UpdateMoneyArriveStatusRequest, ApiResult>(
                        _configuration["CustomerOrderServer:UpdateMoneyArriveStatus"], request);

            return response;
        }

        public async Task<ApiResult> UpdateOrderCompleteStatus(ApiRequest<UpdateCompleteStatusRequest> request)
        {
            var client = _clientFactory.CreateClient("CustomerOrderServer");

            var response =
                await client
                    .PostAsJsonAsync<ApiRequest<UpdateCompleteStatusRequest>, ApiResult>(
                        _configuration["CustomerOrderServer:UpdateOrderCompleteStatus"], request);

            return response;
        }

        /// <summary>
        /// 订单完结发放促销优惠卷-当分享人达到10人下保养订单时即发放指定优惠券
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> SharingPromotion(SharingPromotionRequest request)
        {
            var client = _clientFactory.CreateClient("CustomerOrderServer");

            var response =
                await client
                    .GetAsJsonAsync<SharingPromotionRequest, ApiResult>(
                        _configuration["CustomerOrderServer:SharingPromotion"], request);

            return response;
        }
    }
}
