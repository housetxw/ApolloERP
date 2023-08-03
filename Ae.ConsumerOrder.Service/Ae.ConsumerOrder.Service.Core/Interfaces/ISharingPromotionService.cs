using ApolloErp.Web.WebApi;
using Ae.ConsumerOrder.Service.Core.Request.SharingPromotion;
using Ae.ConsumerOrder.Service.Core.Response.SharingPromotion;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.ConsumerOrder.Service.Core.Interfaces
{
    public interface ISharingPromotionService
    {
        /// <summary>
        /// 得到分享摘要
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<GetSharingSummaryResponse>> GetSharingSummary(GetSharingSummaryRequest request);

        Task<ApiPagedResult<GetSharingOrdersResponse>> GetSharingOrders(GetSharingOrdersRequest request);

        Task<ApiResult<GetSharingCouponResponse>> GetSharingCoupon(GetSharingCouponRequest request);
    }
}
