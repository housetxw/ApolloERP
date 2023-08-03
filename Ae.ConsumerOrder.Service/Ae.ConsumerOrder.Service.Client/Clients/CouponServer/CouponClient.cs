using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ApolloErp.Web.WebApi;
using Ae.ConsumerOrder.Service.Client.Model;
using Ae.ConsumerOrder.Service.Client.Model.Coupon;
using Ae.ConsumerOrder.Service.Client.Request;
using Ae.ConsumerOrder.Service.Client.Request.Coupon;
using Ae.ConsumerOrder.Service.Client.Response;
using Ae.ConsumerOrder.Service.Client.Response.Coupon;

namespace Ae.ConsumerOrder.Service.Client.Clients
{
    public class CouponClient : ICouponClient
    {
        private readonly IHttpClientFactory clientFactory;
        private IConfiguration configuration { get; }

        public CouponClient(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            this.clientFactory = clientFactory;
            this.configuration = configuration;
        }

        public async Task<ApiResult<OrderApplicableCouponEntityResDTO>> GetOrderApplicableCouponList(OrderApplicableCouponListReqDTO request)
        {
            var client = clientFactory.CreateClient("CouponServer");
            var response = await client.PostAsJsonAsync<OrderApplicableCouponListReqDTO, ApiResult<OrderApplicableCouponEntityResDTO>>(configuration["CouponServer:GetOrderApplicableCouponList"], request);
            return response;
        }

        public async Task<ApiResult<bool>> UpdateUserCouponStatusUsedById(UpdateUserCouponStatusReqByIdDTO request)
        {
            var client = clientFactory.CreateClient("CouponServer");
            var response = await client.PostAsJsonAsync<UpdateUserCouponStatusReqByIdDTO, ApiResult<bool>>(configuration["CouponServer:UpdateUserCouponStatusUsedById"], request);
            return response;
        }

        public async Task<ApiResult<bool>> UpdateUserCouponStatusUnusedById(UpdateUserCouponStatusReqByIdDTO request)
        {
            var client = clientFactory.CreateClient("CouponServer");
            var response = await client.PostAsJsonAsync<UpdateUserCouponStatusReqByIdDTO, ApiResult<bool>>(configuration["CouponServer:UpdateUserCouponStatusUnusedById"], request);
            return response;
        }

        public async Task<ApiResult<UserCouponEntityCustomDTO>> GetUserCouponEntityCustomById(UserCouponEntityReqByIdDTO request)
        {
            var client = clientFactory.CreateClient("CouponServer");
            var response = await client.GetAsJsonAsync<UserCouponEntityReqByIdDTO, ApiResult<UserCouponEntityCustomDTO>>(configuration["CouponServer:GetUserCouponEntityCustomById"], request);
            return response;
        }

        public async Task<ApiResult<bool>> AddUserCouponForDiamondMemeber(AddUserCouponForDiamondMemeberRequest request)
        {
            var client = clientFactory.CreateClient("CouponServer");
            var response = await client.PostAsJsonAsync<AddUserCouponForDiamondMemeberRequest, ApiResult<bool>>(configuration["CouponServer:AddUserCouponForDiamondMemeber"], request);
            return response;
        }

        public async Task<ApiResult<bool>> CheckUserCouponValidity(CheckUserCouponValidityRequest request)
        {
            var client = clientFactory.CreateClient("CouponServer");
            var response = await client.GetAsJsonAsync<CheckUserCouponValidityRequest, ApiResult<bool>>(configuration["CouponServer:CheckUserCouponValidity"], request);
            return response;
        }

        public async Task<ApiResult<List<UserCouponPageResByUserIdDTO>>> GetRecommendCourteousCouponList(RecommendCourteousCouponListRequest request)
        {
            var client = clientFactory.CreateClient("CouponServer");
            var response = await client.GetAsJsonAsync<RecommendCourteousCouponListRequest, ApiResult<List<UserCouponPageResByUserIdDTO>>>(configuration["CouponServer:GetRecommendCourteousCouponList"], request);
            return response;
        }

        public async Task<ApiResult<long>> AddRecommendCourteousCoupon(AddRecommendCourteousCouponRequest request)
        {
            var client = clientFactory.CreateClient("CouponServer");
            var response = await client.PostAsJsonAsync<AddRecommendCourteousCouponRequest, ApiResult<long>> (configuration["CouponServer:AddRecommendCourteousCoupon"], request);
            return response;
        }

        public async Task<ApiResult<GiftCouponForOrderResponse>> GiftCouponForOrder(GiftCouponForOrderRequest request)
        {
            var client = clientFactory.CreateClient("CouponServer");
            var response = await client.PostAsJsonAsync<GiftCouponForOrderRequest, ApiResult<GiftCouponForOrderResponse>>(configuration["CouponServer:GiftCouponForOrder"], request);
            return response;
        }
    }
}
