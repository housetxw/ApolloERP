using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.B.Product.Api.Client.Interfaces;
using Ae.B.Product.Api.Client.Model.Order;
using Ae.B.Product.Api.Client.Request.Order;
using Ae.B.Product.Api.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ae.B.Product.Api.Client.Clients
{
    public class OrderClient : IOrderClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ApolloErpLogger<OrderClient> _logger;

        public OrderClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
            ApolloErpLogger<OrderClient> logger)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// 根据优惠券Id查询订单相关信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<OrderCouponDto>> GetOrderCoupons(
            GetOrderCouponsClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("OrderServer");
            ApiResult<List<OrderCouponDto>> result =
                await client
                    .PostAsJsonAsync<GetOrderCouponsClientRequest, ApiResult<List<OrderCouponDto>>>(
                        _configuration["OrderServer:GetOrderCoupons"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data ?? new List<OrderCouponDto>();
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Warn($"GetOrderCoupons_Error {msg}");
                throw new CustomException(msg);
            }
        }
    }
}
