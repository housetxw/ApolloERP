using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Ae.B.Product.Api.Client.Model.Coupon;
using Ae.B.Product.Api.Client.Request.Coupon;
using Ae.B.Product.Api.Core.Model.Coupon;
using Ae.B.Product.Api.Core.Request.Coupon;
using Ae.B.Product.Api.Core.Response;

namespace Ae.B.Product.Api.Client.Interfaces
{
    public interface ICouponClient
    {
        Task<ApiPagedResultData<UserCouponPageResByUserIdDto>> GetUserCouponPageByUserId(
            UserCouponPageByUserIdClientRequest request);

        /// <summary>
        /// 优惠券配置查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<CouponRuleDto>> GetCouponRuleList(GetCouponRuleListClientRequest request);

        /// <summary>
        /// 添加优惠券配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<long> AddCouponRule(AddCouponRuleClientRequest request);

        /// <summary>
        /// 优惠券活动配置查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<CouponActivityDto>> GetCouponActivityList(
            GetCouponActivityListClientRequest request);

        /// <summary>
        /// 新增活动配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<long> AddCouponActivity(AddCouponActivityClientRequest request);

        /// <summary>
        /// 编辑活动配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> EditCouponActivity(EditCouponActivityClientRequest request);

        /// <summary>
        /// 更新活动状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> UpdateCouponActivityStatus(UpdateCouponActivityStatusClientRequest request);

        /// <summary>
        /// 活动详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<CouponActivityDetailDto> GetCouponActivityDetail(CouponActivityDetailClientRequest request);

        /// <summary>
        /// 优惠券发放记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<UserCouponGrantRecordDto>> GetUserCouponGrantRecord(
            UserCouponGrantRecordClientRequest request);

        /// <summary>
        /// 用户塞券
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> GrantUserCoupon(GrantUserCouponClientRequest request);

        /// <summary>
        /// 作废优惠券
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> InvalidatedUserCoupon(InvalidatedUserCouponClientRequest request);

        /// <summary>
        /// 延期优惠券
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> DelayUserCoupon(DelayUserCouponClientRequest request);

        /// <summary>
        /// 更新发放总数量
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> UpdateActivityTotalNum(UpdateActivityTotalNumClientRequest request);

        /// <summary>
        /// 开瓶有奖记录查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<DecapAwardDetailDto>> SearchDecapAward(
            SearchDecapAwardClientRequest request);

        /// <summary>
        /// 消费赠券活动规则列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<GiftCouponRuleDto>> GetGiftCouponRulePageList(
            GiftCouponRulePageListClientRequest request);

        /// <summary>
        /// 消费赠券活动规则详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<GiftCouponRuleDetailDto> GetGiftCouponRuleDetail(GiftCouponRuleDetailClientRequest request);

        /// <summary>
        /// 新增赠券规则
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> AddGiftCouponRule(AddGiftCouponRuleClientRequest request);

        /// <summary>
        /// 编辑赠券规则
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> EditGiftCouponRule(EditGiftCouponRuleClientRequest request);

        Task<ApiPagedResult<GetCouponActivityListForShopResponse>> GetCouponActivityListForShop(
         ApiRequest<GetCouponActivityListForShopRequest> request);

        Task<ApiResult<bool>> SaveShopGrantCoupon(ApiRequest<ShopGrantCouponDTO> request);


    }
}
