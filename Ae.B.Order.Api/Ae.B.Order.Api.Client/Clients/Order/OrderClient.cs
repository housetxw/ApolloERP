using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ApolloErp.Web.WebApi;
using Ae.B.Order.Api.Client.Model;
using Ae.B.Order.Api.Client.Request;
using Ae.B.Order.Api.Client.Response;
using Ae.B.Order.Api.Core.Model.OrderDetail;
using Ae.B.Order.Api.Core.Request;
using Ae.B.Order.Api.Core.Request.OrderCommand;
using Ae.B.Order.Api.Core.Request.OrderQuery;
using Ae.B.Order.Api.Core.Response;
using Ae.B.Order.Api.Core.Response.OrderQuery;

namespace Ae.B.Order.Api.Client.Clients
{
    public class OrderClient : IOrderClient
    {
        private readonly IHttpClientFactory clientFactory;
        private IConfiguration configuration { get; }

        public OrderClient(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            this.clientFactory = clientFactory;
            this.configuration = configuration;
        }

        public async Task<ApiPagedResult<GetOrderListForBossClientResponse>> GetOrderListForBoss(GetOrderListForBossClientRequest request)
        {
            var client = clientFactory.CreateClient("OrderServer");
            var response = await client.GetAsJsonAsync<GetOrderListForBossClientRequest, ApiPagedResult<GetOrderListForBossClientResponse>>(configuration["OrderServer:GetOrderListForBoss"], request);
            return response;
        }

        public async Task<ApiPagedResult<MergePayNoDTO>> GetMergeOrderListForBoss(GetMergeOrderListRequest request)
        {
            var client = clientFactory.CreateClient("PayServer");
            var response = await client.GetAsJsonAsync<GetMergeOrderListRequest, ApiPagedResult<MergePayNoDTO>>(configuration["PayServer:GetMergeOrderList"], request);
            return response;
        }
        /// <summary>
        ///  根据订单编号查询订单信息
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        public async Task<ApiPagedResult<OrderDetailForBossOrderInfoDTO>> GetOrderByNo(string OrderNo)
        {
            var client = clientFactory.CreateClient("OrderServer");
            var response = await client.GetAsJsonAsync<Object, ApiPagedResult<OrderDetailForBossOrderInfoDTO>>(configuration["OrderServer:GetOrderByNo"], new { OrderNo= OrderNo });
            return response;
        }

        public async Task<ApiResult<long>> UpdateOrderStatus(UpdateOrderStatusRequest request)
        {
            var client = clientFactory.CreateClient("OrderServer");
            var response = await client.PostAsJsonAsync<UpdateOrderStatusRequest, ApiResult<long>>(configuration["OrderServer:UpdateOrderStatus"], request);
            return response;
        }
        public async Task<ApiResult<bool>> UpdateCouponAmount(UpdateCouponAmountRequest request)
        {
            var client = clientFactory.CreateClient("OrderServer");
            var response = await client.PostAsJsonAsync<UpdateCouponAmountRequest, ApiResult<bool>>(configuration["OrderServer:UpdateCouponAmount"], request);
            return response;
        }

        public async Task<ApiResult<GetOrderCompleteStaticReportResponse>> GetOrderCompleteStaticReport(GetOrderCompleteStaticReportRequest request)
        {
            var client = clientFactory.CreateClient("OrderServer");
            var response = await client.GetAsJsonAsync<GetOrderCompleteStaticReportRequest, ApiResult<GetOrderCompleteStaticReportResponse>>(configuration["OrderServer:GetOrderCompleteStaticReport"], request);
            return response;
        }

        public async Task<ApiResult<GetOrderNotCompleteStaticReportResponse>> GetOrderNotCompleteStaticReport(GetOrderNotCompleteStaticReportRequest request)
        {
            var client = clientFactory.CreateClient("OrderServer");
            var response = await client.GetAsJsonAsync<GetOrderNotCompleteStaticReportRequest, ApiResult<GetOrderNotCompleteStaticReportResponse>>(configuration["OrderServer:GetOrderNotCompleteStaticReport"], request);
            return response;
        }

        public async Task<ApiPagedResult<GetOrderPackageCardsResponse>> GetOrderPackageCards(GetOrderPackageCardsRequest request)
        {
            var client = clientFactory.CreateClient("OrderServer");
            var response = await client.GetAsJsonAsync<GetOrderPackageCardsRequest, ApiPagedResult<GetOrderPackageCardsResponse>>(configuration["OrderServer:GetOrderPackageCards"], request);
            return response;

        }

        public async Task<ApiPagedResult<GetPackageCardRecordsResponse>> GetPackageCardRecords(GetPackageCardRecordsRequest request)
        {
            var client = clientFactory.CreateClient("OrderServer");
            var response = await client.GetAsJsonAsync<GetPackageCardRecordsRequest, ApiPagedResult<GetPackageCardRecordsResponse>>(configuration["OrderServer:GetPackageCardRecords"], request);
            return response;
        }

        public async Task<ApiResult> UpdateOrderPackage(ApiRequest<UpdateOrderPackageRequest> request)
        {
            var client = clientFactory.CreateClient("OrderServer");
            var response = await client.PostAsJsonAsync<ApiRequest<UpdateOrderPackageRequest>, ApiResult>(configuration["OrderServer:UpdateOrderPackage"], request);
            return response;
        }

        public async Task<ApiResult<List<OrderCarDTO>>> GetOrderCarsInfo(GetOrderCarsRequest request)
        {
            var client = clientFactory.CreateClient("OrderServer");
            var response = await client.PostAsJsonAsync<GetOrderCarsRequest, ApiResult<List<OrderCarDTO>>>(configuration["OrderServer:GetOrderCarsInfo"], request);
            return response;
        }
    }
}
