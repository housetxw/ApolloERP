using Microsoft.Extensions.Configuration;
using ApolloErp.Web.WebApi;
using Ae.B.Order.Api.Client.Model;
using Ae.B.Order.Api.Client.Request;
using Ae.B.Order.Api.Client.Response;
using Ae.B.Order.Api.Core.Request;
using Ae.B.Order.Api.Core.Request.OrderCommand;
using Ae.B.Order.Api.Core.Response.OrderCommand;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Ae.B.Order.Api.Core.Request.OrderQuery;
using Ae.B.Order.Api.Core.Response.OrderQuery;
using Ae.B.Order.Api.Core.Model.OrderDetail;

namespace Ae.B.Order.Api.Client.Clients.ShopOrder
{
    public class ShopOrderClient : IShopOrderClient
    {
        private readonly IHttpClientFactory clientFactory;
        private IConfiguration configuration { get; }

        public ShopOrderClient(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            this.clientFactory = clientFactory;
            this.configuration = configuration;
        }
        public async Task<ApiResult<GetOrderDetailForBossClientResponse>> GetOrderDetailForBoss(GetOrderDetailForBossClientRequest request)
        {
            var client = clientFactory.CreateClient("ShopOrderServer");
            var response = await client.GetAsJsonAsync<GetOrderDetailForBossClientRequest, ApiResult<GetOrderDetailForBossClientResponse>>(configuration["ShopOrderServer:GetOrderDetailForBoss"], request);
            return response;
        }

        public async Task<ApiPagedResult<OrderLogDTO>> GetOrderLogList(GetOrderLogListClientRequest request)
        {
            var client = clientFactory.CreateClient("ShopOrderServer");
            var response = await client.GetAsJsonAsync<GetOrderLogListClientRequest, ApiPagedResult<OrderLogDTO>>(configuration["ShopOrderServer:GetOrderLogList"], request);
            return response;
        }

        public async Task<ApiResult<SubmitOrderResponse>> SubmitOrder(ApiRequest<SubmitOrderRequest> request)
        {
            var client = clientFactory.CreateClient("ShopOrderServer");
            var response = await client.PostAsJsonAsync<ApiRequest<SubmitOrderRequest>, ApiResult<SubmitOrderResponse>>(configuration["ShopOrderServer:SubmitOrder"], request);
            return response;
        }

        public async Task<ApiResult> CancelOrder(ApiRequest<CancelOrderRequest> request)
        {
            var client = clientFactory.CreateClient("ShopOrderServer");
            var response = await client.PostAsJsonAsync<ApiRequest<CancelOrderRequest>, ApiResult>(configuration["ShopOrderServer:CancelOrder"], request);
            return response;
        }

        public async Task<ApiResult> UpdateOrderAddress(ApiRequest<UpdateOrderAddressRequest> request)
        {
            var client = clientFactory.CreateClient("ShopOrderServer");
            var response = await client.PostAsJsonAsync<ApiRequest<UpdateOrderAddressRequest>, ApiResult>(configuration["ShopOrderServer:UpdateOrderAddress"], request);
            return response;
        }

        public async Task<ApiPagedResult<GetOrderDetailStaticReportResponse>> GetOrderDetailStaticReport(GetOrderDetailStaticReportRequest request)
        {
            var client = clientFactory.CreateClient("ShopOrderServer");
            var response = await client.GetAsJsonAsync<GetOrderDetailStaticReportRequest, ApiPagedResult<GetOrderDetailStaticReportResponse>>(configuration["ShopOrderServer:GetOrderDetailStaticReport"], request);
            return response;
        }

        public async Task<ApiPagedResult<VerificationRuleDTO>> GetVerificationRule(GetVerificationRuleRequest request)
        {
            var client = clientFactory.CreateClient("ShopOrderServer");
            var response = await client.GetAsJsonAsync<GetVerificationRuleRequest, ApiPagedResult<VerificationRuleDTO>>(configuration["ShopOrderServer:GetVerificationRule"], request);
            return response;
        }

        public async Task<ApiResult> SaveVerificationRule(ApiRequest<SaveVerificationRuleRequest> request)
        {
            var client = clientFactory.CreateClient("ShopOrderServer");
            var response = await client.PostAsJsonAsync<ApiRequest<SaveVerificationRuleRequest>, ApiResult>(configuration["ShopOrderServer:SaveVerificationRule"], request);
            return response;
        }

        public async Task<ApiResult> SaveBeautiyOrPackageCardVerificationProduct(ApiRequest<SaveBeautiyOrPackageCardVerificationProductRequest> request)
        {
            var client = clientFactory.CreateClient("ShopOrderServer");
            var response = await client.PostAsJsonAsync<ApiRequest<SaveBeautiyOrPackageCardVerificationProductRequest>, ApiResult>(configuration["ShopOrderServer:SaveBeautiyOrPackageCardVerificationProduct"], request);
            return response;
        }

        public async Task<ApiPagedResult<GetOrderOutProductsProfitResponse>> GetOrderOutProductsProfitReport(ApiRequest<GetOrderOutProductsProfitRequest> request)
        {
            var client = clientFactory.CreateClient("ShopOrderServer");
            var response = await client.PostAsJsonAsync<ApiRequest<GetOrderOutProductsProfitRequest>, ApiPagedResult<GetOrderOutProductsProfitResponse>>(configuration["ShopOrderServer:GetOrderOutProductsProfitReport"], request);
            return response;
        }

        public async Task<ApiPagedResult<OrderProductNewDTO>> GetOrderProductsReport(ApiRequest<GetOrderProductsRequest> request)
        {
            var client = clientFactory.CreateClient("ShopOrderServer");
            var response = await client.PostAsJsonAsync<ApiRequest<GetOrderProductsRequest>, ApiPagedResult<OrderProductNewDTO>>(configuration["ShopOrderServer:GetOrderProductsReport"], request);
            return response;
        }

        public async Task<ApiResult<bool>> UpdateOrderCouponAmount(UpdateCouponAmountRequest request)
        {
            var client = clientFactory.CreateClient("ShopOrderServer");
            var response = await client.PostAsJsonAsync<UpdateCouponAmountRequest, ApiResult<bool>>(configuration["ShopOrderServer:UpdateOrderCouponAmount"], request);
            return response;
        }

        public async Task<ApiResult> UpdateOrderProductCostPrice(ApiRequest<BatchUpdateCompleteStatusRequest> request)
        {
            var client = clientFactory.CreateClient("ShopOrderServer");
            var response = await client.PostAsJsonAsync<ApiRequest<BatchUpdateCompleteStatusRequest>, ApiResult>(configuration["ShopOrderServer:UpdateOrderProductCostPrice"], request);
            return response;
        }

        public async Task<ApiResult> UpdateOrderProductActualPayAmount(ApiRequest<BatchUpdateCompleteStatusRequest> request)
        {
            var client = clientFactory.CreateClient("ShopOrderServer");
            var response = await client.PostAsJsonAsync<ApiRequest<BatchUpdateCompleteStatusRequest>, ApiResult>(configuration["ShopOrderServer:UpdateOrderProductActualPayAmount"], request);
            return response;
        }

        public async Task<ApiResult> BatchUpdateOrderProductCostPriceActualPayAmount(ApiRequest<BatchUpdateOrderRequest> request)
        {
            var client = clientFactory.CreateClient("ShopOrderServer");
            var response = await client.PostAsJsonAsync<ApiRequest<BatchUpdateOrderRequest>, ApiResult>(configuration["ShopOrderServer:BatchUpdateOrderProductCostPriceActualPayAmount"], request);
            return response;
        }
        public async Task<ApiResult> BatchUpdateEmployeePerformanceOrder(ApiRequest<BatchUpdateOrderRequest> request)
        {
            var client = clientFactory.CreateClient("ShopOrderServer");
            var response = await client.PostAsJsonAsync<ApiRequest<BatchUpdateOrderRequest>, ApiResult>(configuration["ShopOrderServer:BatchUpdateEmployeePerformanceOrder"], request);
            return response;
        }
    }
}
