using ApolloErp.Web.WebApi;
using Ae.ConsumerOrder.Service.Client.Model;
using Ae.ConsumerOrder.Service.Client.Model.Coupon;
using Ae.ConsumerOrder.Service.Client.Request;
using Ae.ConsumerOrder.Service.Client.Request.Coupon;
using Ae.ConsumerOrder.Service.Client.Response;
using Ae.ConsumerOrder.Service.Client.Response.Coupon;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.ConsumerOrder.Service.Client.Clients
{
    public interface ICouponClient
    {
        /// <summary>
        /// 查询订单确认页可用优惠券列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<OrderApplicableCouponEntityResDTO>> GetOrderApplicableCouponList(OrderApplicableCouponListReqDTO request);
        /// <summary>
        /// 根据userCouponId和userId更新用户优惠券为已使用
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<bool>> UpdateUserCouponStatusUsedById(UpdateUserCouponStatusReqByIdDTO request);
        /// <summary>
        /// 根据userCouponId和userId更新用户优惠券为未使用状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<bool>> UpdateUserCouponStatusUnusedById(UpdateUserCouponStatusReqByIdDTO request);
        /// <summary>
        /// 根据用户优惠券Id，获取其自定义详细信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<UserCouponEntityCustomDTO>> GetUserCouponEntityCustomById(UserCouponEntityReqByIdDTO request);

        /// <summary>
        /// 钻石会员发放优惠券
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<bool>> AddUserCouponForDiamondMemeber(AddUserCouponForDiamondMemeberRequest request);

        /// <summary>
        /// 钻石会员发放优惠券
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<bool>> CheckUserCouponValidity(CheckUserCouponValidityRequest request );

        Task<ApiResult<List<UserCouponPageResByUserIdDTO>>> GetRecommendCourteousCouponList(
           RecommendCourteousCouponListRequest request);

        Task<ApiResult<long>> AddRecommendCourteousCoupon(
            AddRecommendCourteousCouponRequest request);

        Task<ApiResult<GiftCouponForOrderResponse>> GiftCouponForOrder(
            GiftCouponForOrderRequest request);
    }
}
