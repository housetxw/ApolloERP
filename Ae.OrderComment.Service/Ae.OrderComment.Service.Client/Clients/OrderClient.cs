using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.OrderComment.Service.Client.Interface;
using Ae.OrderComment.Service.Client.Model;
using Ae.OrderComment.Service.Client.Request;
using Ae.OrderComment.Service.Client.Response;
using Ae.OrderComment.Service.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ae.OrderComment.Service.Client.Clients
{
    public class OrderClient : IOrderClient
    {
        private readonly IHttpClientFactory clientFactory;
        private IConfiguration configuration { get; }

        private readonly ApolloErpLogger<OrderClient> _logger;

        public OrderClient(IHttpClientFactory clientFactory, IConfiguration configuration,
            ApolloErpLogger<OrderClient> logger)
        {
            this.clientFactory = clientFactory;
            this.configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// 根据订单号查询订单信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<GetOrderDetailClientResponse>> GetOrderDetail(GetOrderDetailClientRequest request)
        {
            var client = clientFactory.CreateClient("ConsumerOrderServer");
            ApiResult<GetOrderDetailClientResponse> response = await client.GetAsJsonAsync<GetOrderDetailClientRequest, ApiResult<GetOrderDetailClientResponse>>(configuration["ConsumerOrderServer:GetOrderDetail"], request);
            return response;
        }
        /// <summary>
        ///  根据订单号查询车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<OrderCarDTO>> GetCarByOrderNo(GetCarByOrderNoClientRequest request)
        {
            var client = clientFactory.CreateClient("ConsumerOrderServer");
            ApiResult<OrderCarDTO> response = await client.GetAsJsonAsync<GetCarByOrderNoClientRequest, ApiResult<OrderCarDTO>>(configuration["ConsumerOrderServer:GetCarByOrderNo"], request);
            return response;
        }
        /// <summary>
        /// 根据门店ID批量获取订单数量
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<BatchGetOrderCountByShopIdResponse>>> BatchGetOrderCountByShopId(BatchGetOrderCountByShopIdRequest request)
        {
            var client = clientFactory.CreateClient("OrderServer");
            var response = await client.PostAsJsonAsync<BatchGetOrderCountByShopIdRequest, ApiResult<List<BatchGetOrderCountByShopIdResponse>>>(configuration["OrderServer:BatchGetOrderCountByShopId"], request);
            return response;
        }

        public async Task<long> UpdateOrderStatus(UpdateOrderStatusClientRequest request)
        {
            var client = clientFactory.CreateClient("OrderServer");
            var response =
                await client
                    .PostAsJsonAsync<UpdateOrderStatusClientRequest,
                        ApiResult<long>>(
                        configuration["OrderServer:UpdateOrderStatus"], request);
            if (response.IsNotNullSuccess())
            {
                return response.Data;
            }
            else
            {
                var msg = response?.Message ?? "系统异常";
                _logger.Info($"UpdateOrderStatus_Error {msg}");
                throw new CustomException(msg);
            }
        }

        public async Task<ApiResult<GetFreshFramerOrderDetailResponse>> GetFreshFramerOrderDetail(GetFreshFramerOrderDetailRequest request)
        {
            var client = clientFactory.CreateClient("ShopOrderServer");
            var response = await client.GetAsJsonAsync<GetFreshFramerOrderDetailRequest, ApiResult<GetFreshFramerOrderDetailResponse>>(configuration["ShopOrderServer:GetFreshFramerOrderDetail"], request);
            return response;
        }
    }
}
