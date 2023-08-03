using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Core.Request.SharingPromotion;
using Ae.C.MiniApp.Api.Core.Response.SharingPromotion;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.C.MiniApp.Api.Core.Interfaces
{
    public interface ISharingPromotionService
    {
        Task<ApiResult<GetSharingSummaryResponse>> GetSharingSummary(GetSharingSummaryRequest request);

        Task<ApiResult<List<string>>> GetSharingRuleDescription();

        Task<ApiPagedResult<GetSharingOrdersResponse>> GetSharingOrders(GetSharingOrdersRequest request);


        Task<ApiResult<GetSharingCouponResponse>> GetSharingCoupon(GetSharingCouponRequest request);
    }
}
