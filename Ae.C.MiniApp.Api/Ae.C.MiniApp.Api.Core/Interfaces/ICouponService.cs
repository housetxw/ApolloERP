using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Core.Request.Coupon;
using Ae.C.MiniApp.Api.Core.Response.Coupon;

namespace Ae.C.MiniApp.Api.Core.Interfaces
{
    public interface ICouponService
    {
        #region UserCoupon

        Task<ApiPagedResult<UserCouponPageResByUserIdVO>> GetUserCouponPageByUserId(UserCouponPageReqByUserIdVO req);

        Task<ApiResult<bool>> ExchangeCouponByPromotionCode(ApiRequest<ExchangeCouponReqByCodeVO> req);

        Task<ApiResult<bool>> IntegralExchangeCoupon(ApiRequest<IntegralExchangeCouponReqVO> req);

        /// <summary>
        /// 通过活动领取优惠券
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> ExchangeCouponByActId(ExchangeCouponByActIdRequest request);

        #endregion

        #region CouponActivity

        Task<ApiPagedResult<CouponActivityPageResVO>> GetIntegralCouponActivityPage(CouponActivityPageReqByChannelVOForDTO req);

        /// <summary>
        /// 兑换码查询优惠券
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<CouponActivityByCodeResponse> GetCouponActivityByCode(CouponActivityByCodeRequest request);

        /// <summary>
        /// 根据活动获取优惠券详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<CouponDetailByActIdResponse> GetCouponDetailByActId(CouponDetailByActIdRequest request);

        /// <summary>
        /// 根据产品获取可领取的优惠券
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<GetCouponListByProductResponse> GetCouponListByProduct(CouponListByProductRequest request);

        #endregion

        #region CouponRule

        #endregion

        #region 开瓶有礼

        /// <summary>
        /// 扫码查询开瓶有礼
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<DecapAwardByCodeResponse> GetDecapAwardByCode(DecapAwardByCodeRequest request);

        /// <summary>
        /// 开瓶有奖数据查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<DecapAwardByCodeResponse> GetDecapAwardDetailByCode(DecapAwardDetailByCodeRequest request);

        /// <summary>
        /// 领取红包
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> DrawDecapAward(DrawDecapAwardRequest request);

        /// <summary>
        /// 领取红包V2
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<DrawDecapAwardV2Response> DrawDecapAwardV2(DrawDecapAwardRequest request);

        #endregion
    }
}
