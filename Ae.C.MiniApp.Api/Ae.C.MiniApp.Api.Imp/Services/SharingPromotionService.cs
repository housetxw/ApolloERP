using AutoMapper;
using ApolloErp.Log;
using ApolloErp.Login.Auth;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Interface;
using Ae.C.MiniApp.Api.Core.Interfaces;
using Ae.C.MiniApp.Api.Core.Request.SharingPromotion;
using Ae.C.MiniApp.Api.Core.Response.SharingPromotion;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.C.MiniApp.Api.Imp.Services
{
    public class SharingPromotionService : ISharingPromotionService
    {
        private readonly IMapper mapper;
        private readonly ApolloErpLogger<SharingPromotionService> logger;
        private readonly IIdentityService identityService;
        private readonly IConsumerOrderQueryClient consumerOrderQueryClient;
        private readonly IUserClient userClient;


        public SharingPromotionService(IMapper mapper, ApolloErpLogger<SharingPromotionService> logger,
            IIdentityService identityService, IConsumerOrderQueryClient consumerOrderQueryClient, IUserClient userClient)
        {
            this.mapper = mapper;
            this.logger = logger;
            this.identityService = identityService;
            this.consumerOrderQueryClient = consumerOrderQueryClient;
            this.userClient = userClient;
        }

        public async Task<ApiResult<GetSharingCouponResponse>> GetSharingCoupon(GetSharingCouponRequest request)
        {
            request.UserId = identityService.GetUserId();
            //request.UserId = "1";
            return await consumerOrderQueryClient.GetSharingCoupon(request);
        }

        public async Task<ApiPagedResult<GetSharingOrdersResponse>> GetSharingOrders(GetSharingOrdersRequest request)
        {
            request.UserId = identityService.GetUserId();
            //request.UserId = "1";
            return await consumerOrderQueryClient.GetSharingOrders(request);

        }

        public async Task<ApiResult<List<string>>> GetSharingRuleDescription()
        {

            return await consumerOrderQueryClient.GetSharingRuleDescription();
        }

        public async Task<ApiResult<GetSharingSummaryResponse>> GetSharingSummary(GetSharingSummaryRequest request)
        {
            request.UserId = identityService.GetUserId();
            //request.UserId = "1";

            var getSharingSummaryResponse = await consumerOrderQueryClient.GetSharingSummary(request);
            if (string.IsNullOrWhiteSpace(getSharingSummaryResponse.Data.UserName))
            {
                getSharingSummaryResponse.Data.UserName = identityService.GetUserName();
            }


            var getReferrerCustomerNum = await userClient.GetReferrerCustomerNum(new Client.Request.BaseUserClientRequest()
            { 
                UserId = request.UserId
            });

            getSharingSummaryResponse.Data.ReferrerUserNum = getReferrerCustomerNum;

            return getSharingSummaryResponse;

        }
    }
}
