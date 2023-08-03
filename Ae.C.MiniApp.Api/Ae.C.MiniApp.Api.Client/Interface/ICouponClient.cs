using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Model.Coupon;
using Ae.C.MiniApp.Api.Client.Request.Coupon;
using Ae.C.MiniApp.Api.Client.Response.Coupon;
using Ae.C.MiniApp.Api.Core.Model.Coupon;
using Ae.C.MiniApp.Api.Core.Request.Coupon;

namespace Ae.C.MiniApp.Api.Client.Interface
{
    public interface ICouponClient
    {
        #region UserCoupon

        Task<ApiPagedResult<UserCouponPageResByUserIdDTO>> GetUserCouponPageByUserId(UserCouponPageReqByUserIdDTO req);

        Task<ApiResult<bool>> ExchangeCouponByPromotionCode(ApiRequest<ExchangeCouponReqByCodeDTO> req);

        Task<ApiResult<bool>> IntegralExchangeCoupon(ApiRequest<IntegralExchangeCouponReqDTO> req);

        /// <summary>
        ///  通过活动领取优惠券【一般活动：不需要积分，不需要兑换码】
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> ExchangeCouponByActId(ExchangeCouponByActIdClientRequest request);

        #endregion

        #region CouponActivity

        Task<ApiPagedResult<CouponActivityPageResDTO>> GetIntegralCouponActivityPage(CouponActivityPageReqByChannelDTO req);

        /// <summary>
        /// 兑换码查询优惠券
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<CouponActivityByCodeClientResponse> GetCouponActivityByCode(
            CouponActivityByCodeClientRequest request);

        #endregion

        #region CouponRule

        #endregion

        /// <summary>
        /// 用户可用优惠券
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<int> GetUserCouponUnusedAmountByUserId(UserCouponUnusedAmountByUserIdRequest request);

        Task<ApiResult<List<CouponProductVo>>> GetRecommendProductsForDiamondMembership(
            RecommendProductsForDiamondMembershipRequest request);

        /// <summary>
        /// 查询开瓶有奖信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<DecapAwardDto> GetDecapAwardByCode(DecapAwardByCodeClientRequest request);

        /// <summary>
        /// 领取奖励
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> DrawDecapAward(DrawDecapAwardClientRequest request);

        /// <summary>
        /// 根据活动获取优惠券详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<CouponDetailByActIdClientResponse> GetCouponDetailByActId(
            CouponDetailByActIdClientRequest request);

        /// <summary>
        /// 根据产品获取可领取的优惠券
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<GetCouponListByProductClientResponse> GetCouponListByProduct(
            CouponListByProductClientRequest request);
    }
}
