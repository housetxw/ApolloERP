using ApolloErp.Web.WebApi;
using Ae.ShopOrder.Service.Client.Request.Coupon;
using Ae.ShopOrder.Service.Client.Response.Coupon;
using Ae.ShopOrder.Service.Core.Model.Order;
using Ae.ShopOrder.Service.Core.Request.Order;
using Ae.ShopOrder.Service.Core.Response.Order;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.ShopOrder.Service.Client.Clients.Coupon
{
    public interface ICouponClient
    {
      
        /// <summary>
        /// 根据userCouponId和userId更新用户优惠券为已使用
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<bool>> UpdateUserCouponStatusUsedById(UpdateUserCouponStatusReqByIdRequest request);
        /// <summary>
        /// 根据userCouponId和userId更新用户优惠券为未使用状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<bool>> UpdateUserCouponStatusUnusedById(UpdateUserCouponStatusReqByIdRequest request);
        /// <summary>
        /// 根据用户优惠券Id，获取其自定义详细信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<UserCouponEntityCustomResponse>> GetUserCouponEntityCustomById(UserCouponEntityReqByIdRequest request);

        /// <summary>
        /// 释放优惠卷
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<bool>> UpdateUserCouponStatusUnusedById(UpdateUserCouponStatusUnusedByIdRequest request);

        /// <summary>
        /// 查询订单确认页可用优惠券列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<OrderApplicableCouponEntityResDTO>> GetOrderApplicableCouponList(OrderApplicableCouponListReqDTO request);

        Task<ApiPagedResult<UserCouponVO>> GetUserCouponPageByUserId(GetUserCouponPageByUserIdRequest request);


        Task<ApiResult<GiftCouponForOrderResponse>> GiftCouponForOrder(
            GiftCouponForOrderRequest request);
    }
}
