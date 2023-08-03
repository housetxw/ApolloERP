using Ae.C.MiniApp.Api.Client.Request;
using Ae.C.MiniApp.Api.Client.Response;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Ae.C.MiniApp.Api.Client.Interface;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Request.Order;
using Ae.C.MiniApp.Api.Client.Response.Order;
using Ae.C.MiniApp.Api.Core.Request.OrderQuery;
using Ae.C.MiniApp.Api.Core.Response.OrderQuery;
using System.Collections.Generic;
using Ae.C.MiniApp.Api.Core.Request.SharingPromotion;
using Ae.C.MiniApp.Api.Core.Response.SharingPromotion;

namespace Ae.C.MiniApp.Api.Client.Clients.Order
{
    public class ConsumerOrderQueryClient : IConsumerOrderQueryClient
    {
        private readonly IHttpClientFactory clientFactory;
        private IConfiguration configuration { get; }
        public ConsumerOrderQueryClient(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            this.clientFactory = clientFactory;
            this.configuration = configuration;
        }

        public async Task<ApiResult<GetOrderConfirmClientResponse>> GetOrderConfirm(GetOrderConfirmClientRequest request)
        {
            var client = clientFactory.CreateClient("ConsumerOrderServer");
            var response = await client.PostAsJsonAsync<GetOrderConfirmClientRequest, ApiResult<GetOrderConfirmClientResponse>>(configuration["ConsumerOrderServer:GetOrderConfirm"], request);
            return response;
        }

        public async Task<ApiResult<TrialCalcOrderAmountClientResponse>> TrialCalcOrderAmount(TrialCalcOrderAmountClientRequest request)
        {
            var client = clientFactory.CreateClient("ConsumerOrderServer");
            var response = await client.PostAsJsonAsync<TrialCalcOrderAmountClientRequest, ApiResult<TrialCalcOrderAmountClientResponse>>(configuration["ConsumerOrderServer:TrialCalcOrderAmount"], request);
            return response;
        }

        public async Task<ApiResult<GetOrderDetailClientResponse>> GetOrderDetail(GetOrderDetailClientRequest request)
        {
            var client = clientFactory.CreateClient("ConsumerOrderServer");
            var response = await client.GetAsJsonAsync<GetOrderDetailClientRequest, ApiResult<GetOrderDetailClientResponse>>(configuration["ConsumerOrderServer:GetOrderDetail"], request);
            return response;
        }

        public async Task<ApiResult<GetOrderVerificationCodeInfosClientResponse>> GetOrderVerificationCodeInfos(GetOrderVerificationCodeInfosClientRequest request)
        {
            var client = clientFactory.CreateClient("ConsumerOrderServer");
            var response = await client.GetAsJsonAsync<GetOrderVerificationCodeInfosClientRequest, ApiResult<GetOrderVerificationCodeInfosClientResponse>>(configuration["ConsumerOrderServer:GetOrderVerificationCodeInfos"], request);
            return response;
        }

        public async Task<ApiResult<GetOrderServiceTypeResponse>> GetOrderServiceType(GetOrderServiceTypeRequest request)
        {
            var client = clientFactory.CreateClient("ConsumerOrderServer");
            var response = await client.GetAsJsonAsync<GetOrderServiceTypeRequest, ApiResult<GetOrderServiceTypeResponse>>(configuration["ConsumerOrderServer:GetOrderServiceType"], request);
            return response;
        }

        public async Task<ApiResult<GetSharingSummaryResponse>> GetSharingSummary(GetSharingSummaryRequest request)
        {
            var client = clientFactory.CreateClient("ConsumerOrderServer");
            var response = await client.GetAsJsonAsync<GetSharingSummaryRequest, ApiResult<GetSharingSummaryResponse>>(configuration["ConsumerOrderServer:GetSharingSummary"], request);
            return response;
        }

        public async Task<ApiResult<List<string>>> GetSharingRuleDescription()
        {
            var client = clientFactory.CreateClient("ConsumerOrderServer");
            var response = await client.GetAsJsonAsync<ApiResult<List<string>>>(configuration["ConsumerOrderServer:GetSharingRuleDescription"], string.Empty);
            return response;
        }

        public async Task<ApiPagedResult<GetSharingOrdersResponse>> GetSharingOrders(GetSharingOrdersRequest request)
        {
            var client = clientFactory.CreateClient("ConsumerOrderServer");
            var response = await client.GetAsJsonAsync<GetSharingOrdersRequest, ApiPagedResult<GetSharingOrdersResponse>>(configuration["ConsumerOrderServer:GetSharingOrders"], request);
            return response;
        }

        public async Task<ApiResult<GetSharingCouponResponse>> GetSharingCoupon(GetSharingCouponRequest request)
        {
            var client = clientFactory.CreateClient("ConsumerOrderServer");
            var response = await client.GetAsJsonAsync<GetSharingCouponRequest, ApiResult<GetSharingCouponResponse>>(configuration["ConsumerOrderServer:GetSharingCoupon"], request);
            return response;
        }

        public async Task<ApiResult<GetPackageVerificationCodeDetailResponse>> GetPackageVerificationCodeDetail(GetPackageVerificationCodeDetailRequest request)
        {
            var client = clientFactory.CreateClient("ConsumerOrderServer");
            var response = await client.GetAsJsonAsync<GetPackageVerificationCodeDetailRequest, ApiResult<GetPackageVerificationCodeDetailResponse>>(configuration["ConsumerOrderServer:GetPackageVerificationCodeDetail"], request);
            return response;
        }

        public async Task<ApiResult<GetOrderServiceTypeResponse>> GetOrderServiceTypeV2(GetOrderServiceTypeRequest request)
        {
            var client = clientFactory.CreateClient("ConsumerOrderServer");
            var response = await client.PostAsJsonAsync<GetOrderServiceTypeRequest, ApiResult<GetOrderServiceTypeResponse>>(configuration["ConsumerOrderServer:GetOrderServiceTypeV2"], request);
            return response;
        }
    }
}
