using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.ShopOrder.Service.Client.Request.Coupon;
using Ae.ShopOrder.Service.Client.Response.Coupon;
using Ae.ShopOrder.Service.Core.Model.Order;
using Ae.ShopOrder.Service.Core.Request.Order;
using Ae.ShopOrder.Service.Core.Response.Order;

namespace Ae.ShopOrder.Service.Client.Clients.Coupon.Impl
{
    public class CouponClient : ICouponClient
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly ApolloErpLogger<CouponClient> logger;
        private readonly IConfiguration configuration;
        private readonly HttpClient client;

        public CouponClient(IHttpClientFactory clientFactory, ApolloErpLogger<CouponClient> logger, IConfiguration configuration)
        {
            this.clientFactory = clientFactory;
            client = clientFactory.CreateClient("CouponServer");
            this.configuration = configuration;
            this.logger = logger;
        }

        public async Task<ApiResult<OrderApplicableCouponEntityResDTO>> GetOrderApplicableCouponList(OrderApplicableCouponListReqDTO request)
        {
            var client = clientFactory.CreateClient("CouponServer");
            var response = await client.PostAsJsonAsync<OrderApplicableCouponListReqDTO, ApiResult<OrderApplicableCouponEntityResDTO>>(configuration["CouponServer:GetOrderApplicableCouponList"], request);
            return response;
        }

        public async Task<ApiResult<UserCouponEntityCustomResponse>> GetUserCouponEntityCustomById(UserCouponEntityReqByIdRequest request)
        {
            var client = clientFactory.CreateClient("CouponServer");
            var response = await client.GetAsJsonAsync<UserCouponEntityReqByIdRequest, ApiResult<UserCouponEntityCustomResponse>>(configuration["CouponServer:GetUserCouponEntityCustomById"], request);
            return response;
        }

        public async Task<ApiPagedResult<UserCouponVO>> GetUserCouponPageByUserId(GetUserCouponPageByUserIdRequest request)
        {
            var client = clientFactory.CreateClient("CouponServer");
            var response = await client.PostAsJsonAsync<GetUserCouponPageByUserIdRequest, ApiPagedResult<UserCouponVO>>(configuration["CouponServer:GetUserCouponPageByUserId"],request);
            return response;
        }

        public async Task<ApiResult<GiftCouponForOrderResponse>> GiftCouponForOrder(GiftCouponForOrderRequest request)
        {
            var client = clientFactory.CreateClient("CouponServer");
            var response = await client.PostAsJsonAsync<GiftCouponForOrderRequest, ApiResult<GiftCouponForOrderResponse>>(configuration["CouponServer:GiftCouponForOrder"], request);
            return response;
        }

        public async Task<ApiResult<bool>> UpdateUserCouponStatusUnusedById(UpdateUserCouponStatusReqByIdRequest request)
        {
            var client = clientFactory.CreateClient("CouponServer");
            var response = await client.PostAsJsonAsync<UpdateUserCouponStatusReqByIdRequest, ApiResult<bool>>(configuration["CouponServer:UpdateUserCouponStatusUnusedById"], request);
            return response;
        }

        public async Task<ApiResult<bool>> UpdateUserCouponStatusUnusedById(UpdateUserCouponStatusUnusedByIdRequest request)
        {
            var client = clientFactory.CreateClient("CouponServer");
            var response = await client.PostAsJsonAsync<UpdateUserCouponStatusUnusedByIdRequest, ApiResult<bool>>(configuration["CouponServer:UpdateUserCouponStatusUnusedById"], request);
            return response;
        }

        public async Task<ApiResult<bool>> UpdateUserCouponStatusUsedById(UpdateUserCouponStatusReqByIdRequest request)
        {
            var client = clientFactory.CreateClient("CouponServer");
            var response = await client.PostAsJsonAsync<UpdateUserCouponStatusReqByIdRequest, ApiResult<bool>>(configuration["CouponServer:UpdateUserCouponStatusUsedById"], request);
            return response;
        }
    }
}
