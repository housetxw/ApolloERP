using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ApolloErp.Web.WebApi;
using Ae.ShopOrder.Service.Client.Model.Order;
using Ae.ShopOrder.Service.Client.Request.Order;
using Ae.ShopOrder.Service.Core.Model.Order;
using Ae.ShopOrder.Service.Core.Request;
using Ae.ShopOrder.Service.Core.Request.Order;
using Ae.ShopOrder.Service.Core.Request.OrderForC;

namespace Ae.ShopOrder.Service.Client.Clients.Order.Impl
{
    /// <summary>
    /// 订单中心基本服务
    /// </summary>
    public class OrderClient : IOrderClient
    {
        private readonly IHttpClientFactory _clientFactory;
        private object configuration;

        private IConfiguration _configuration { get; }

        public OrderClient(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
        }
        /// <summary>
        /// 查询订单基本信息根据业务状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResult<OrderDTO>> GetOrderBaseInfoForBusinessStatus(GetOrderBaseInfoForBusinessStatusRequest request)
        {
            var client = _clientFactory.CreateClient("OrderServer");

            var response =
                await client
                    .PostAsJsonAsync<GetOrderBaseInfoForBusinessStatusRequest, ApiPagedResult<OrderDTO>>(
                        _configuration["OrderServer:GetOrderBaseInfoForBusinessStatus"], request);

            return response;
        }

        /// <summary>
        /// 得到订单基本信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<OrderDTO>>> GetOrderBaseInfo(GetOrderBaseInfoRequest request)
        {
            var client = _clientFactory.CreateClient("OrderServer");

            var response =
                await client
                    .PostAsJsonAsync<GetOrderBaseInfoRequest, ApiResult<List<OrderDTO>>>(
                        _configuration["OrderServer:GetOrderBaseInfo"], request);

            return response;
        }

        /// <summary>
        /// 得到订单详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<OrderProductDTO>>> GetOrderProduct(GetOrderProductRequest request)
        {
            var client = _clientFactory.CreateClient("OrderServer");

            var response =
                await client
                    .PostAsJsonAsync<GetOrderProductRequest, ApiResult<List<OrderProductDTO>>>(
                        _configuration["OrderServer:GetOrderProduct"], request);

            return response;
        }

        /// <summary>
        /// 同步订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> SyncOrder(SyncOrderRequest request)
        {
            var client = _clientFactory.CreateClient("OrderServer");
            var response = await client.PostAsJsonAsync<SyncOrderRequest, ApiResult>(_configuration["OrderServer:SyncOrder"], request);
            return response;
        }

        /// <summary>
        /// 取消订单为预约或到店
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> CancelOrderForReserverOrArrival(CancelOrderForReserverOrArrivalRequest request)
        {
            var client = _clientFactory.CreateClient("OrderServer");

            var response =
                await client
                    .PostAsJsonAsync<CancelOrderForReserverOrArrivalRequest, ApiResult>(
                        _configuration["OrderServer:CancelOrderForReserverOrArrival"], request);

            return response;
        }

        /// <summary>
        /// 批量更新付款状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> BatchUpdatePayStatus(BatchUpdatePayStatusRequest request)
        {
            var client = _clientFactory.CreateClient("OrderServer");

            var response =
                await client
                    .PostAsJsonAsync<BatchUpdatePayStatusRequest, ApiResult>(
                        _configuration["OrderServer:BatchUpdatePayStatus"], request);

            return response;
        }


        public async Task<ApiResult> OrderReverseNotify(OrderReverseNotifyRequest request)
        {
            var client = _clientFactory.CreateClient("OrderServer");
            var response = await client.PostAsJsonAsync<OrderReverseNotifyRequest, ApiResult>(_configuration["OrderServer:OrderReverseNotify"], request);
            return response;
        }

        /// <summary>
        /// 更新订单的派工状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> UpdateOrderDispatchStatus(UpdateOrderDispatchStatusRequest request)
        {
            var client = _clientFactory.CreateClient("OrderServer");

            var response =
                await client
                    .PostAsJsonAsync<UpdateOrderDispatchStatusRequest, ApiResult>(
                        _configuration["OrderServer:UpdateOrderDispatchStatus"], request);

            return response;
        }

        /// <summary>
        /// 修改订单完结状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> BatchUpdateCompleteStatus(BatchUpdateCompleteStatusRequest request)
        {
            var client = _clientFactory.CreateClient("OrderServer");
            var response = await client.PostAsJsonAsync<BatchUpdateCompleteStatusRequest, ApiResult>(_configuration["OrderServer:BatchUpdateCompleteStatus"], request);
            return response;
        }

        /// 修改订单安装状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> BatchUpdateInstallStatus(BatchUpdateInstallStatusRequest request)
        {
            var client = _clientFactory.CreateClient("OrderServer");

            var response =
                await client
                    .PostAsJsonAsync<BatchUpdateInstallStatusRequest, ApiResult>(
                        _configuration["OrderServer:BatchUpdateInstallStatus"], request);
            return response;
        }
        public async Task<ApiResult> UpdateProductStockStatus(UpdateProductStockStatusRequest request)
        {
            var client = _clientFactory.CreateClient("OrderServer");
            var response = await client.PostAsJsonAsync<UpdateProductStockStatusRequest, ApiResult>(_configuration["OrderServer:UpdateProductStockStatus"], request);
            return response;
        }

        public async Task<ApiResult> UpdateOrderStockStatus(UpdateOrderStockStatusRequest request)
        {
            var client = _clientFactory.CreateClient("OrderServer");
            var response = await client.PostAsJsonAsync<UpdateOrderStockStatusRequest, ApiResult>(_configuration["OrderServer:UpdateOrderStockStatus"], request);
            return response;
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> CancelOrder(ApiRequest<CancelOrderRequest> request)
        {
            var client = _clientFactory.CreateClient("OrderServer");
            var response = await client.PostAsJsonAsync<ApiRequest<CancelOrderRequest>, ApiResult>(_configuration["OrderServer:CancelOrder"], request);
            return response;
        }

        /// <summary>
        /// 修改订单的支付状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> UpdatePayStatus(UpdatePayStatusRequest request)
        {
            var client = _clientFactory.CreateClient("OrderServer");
            var response = await client.PostAsJsonAsync<UpdatePayStatusRequest, ApiResult>(_configuration["OrderServer:UpdatePayStatus"], request);
            return response;
        }

        /// <summary>
        /// 修改订单的到账状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> UpdateMoneyArriveStatus(UpdateMoneyArriveStatusRequest request)
        {
            var client = _clientFactory.CreateClient("OrderServer");
            var response = await client.PostAsJsonAsync<UpdateMoneyArriveStatusRequest, ApiResult>(_configuration["OrderServer:UpdateMoneyArriveStatus"], request);
            return response;
        }

        public async Task<ApiResult<long>> UpdateOrderStatus(UpdateOrderStatusRequest request)
        {
            var client = _clientFactory.CreateClient("OrderServer");
            var response = await client.PostAsJsonAsync<UpdateOrderStatusRequest, ApiResult<long>>(_configuration["OrderServer:UpdateOrderStatus"], request);
            return response;
        }

        public async Task<ApiResult> UpdateOrderCompleteStatus(ApiRequest<UpdateCompleteStatusRequest> request)
        {
            var client = _clientFactory.CreateClient("OrderServer");
            var response = await client.PostAsJsonAsync<ApiRequest<UpdateCompleteStatusRequest>, ApiResult>(_configuration["OrderServer:UpdateOrderCompleteStatus"], request);
            return response;
        }

        public async Task<ApiPagedResult<OrderDTO>> GetNotPayHaveStockOrder(GetNotPayOrderRequest request)
        {
            var client = _clientFactory.CreateClient("OrderServer");
            var response = await client.GetAsJsonAsync<GetNotPayOrderRequest, ApiPagedResult<OrderDTO>>(_configuration["OrderServer:GetNotPayHaveStockOrder"], request);
            return response;
        }
    }
}
