using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Ae.B.Product.Api.Core.Model.Coupon;
using Ae.B.Product.Api.Core.Model.User;
using Ae.B.Product.Api.Core.Request.Coupon;
using Ae.B.Product.Api.Core.Request.User;

namespace Ae.B.Product.Api.Core.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICouponService
    {
        /// <summary>
        /// 优惠券列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<UserCouponPageResByUserIdVo>> GetUserCouponPageByUserId(
            UserCouponPageByUserIdRequest request);

        /// <summary>
        /// 优惠券配置查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<CouponRuleVo>> GetCouponRuleList(GetCouponRuleListRequest request);

        /// <summary>
        /// 获取优惠券类型
        /// </summary>
        /// <returns></returns>
        Task<List<CouponTypeVo>> GetCouponTypeEnum(CouponTypeEnumRequest request);

        /// <summary>
        /// 获取券分类
        /// </summary>
        /// <returns></returns>
        Task<List<CouponTypeVo>> GetCouponCategory();

        /// <summary>
        /// 获取券类型
        /// </summary>
        /// <returns></returns>
        Task<List<CouponTypeVo>> GetCouponType();

        /// <summary>
        /// 获取优惠券规则
        /// </summary>
        /// <returns></returns>
        Task<List<CouponTypeVo>> GetCouponRangeType();

        /// <summary>
        /// 获取支付方式
        /// </summary>
        /// <returns></returns>
        Task<List<CouponTypeVo>> GetPayMethod();

        /// <summary>
        /// 有效期开始类型
        /// </summary>
        /// <returns></returns>
        Task<List<CouponTypeVo>> GetValidStartType();

        /// <summary>
        /// 有效期结束类型
        /// </summary>
        /// <returns></returns>
        Task<List<CouponTypeVo>> GetValidEndType();

        /// <summary>
        /// 添加优惠券配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<long> AddCouponRule(AddCouponRuleRequest request);

        /// <summary>
        /// 获取活动配置列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<CouponActivityVo>> GetCouponActivityList(
            GetCouponActivityListRequest request);

        /// <summary>
        /// 添加活动配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<long> AddCouponActivity(AddCouponActivityRequest request);

        /// <summary>
        /// 编辑活动配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> EditCouponActivity(EditCouponActivityRequest request);

        /// <summary>
        /// 活动渠道
        /// </summary>
        /// <returns></returns>
        Task<List<CouponTypeVo>> GetCouponActivityChannel();

        /// <summary>
        /// 需要积分类型
        /// </summary>
        /// <returns></returns>
        Task<List<CouponTypeVo>> GetCouponActivityIntegralType();

        /// <summary>
        /// 更新活动状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> UpdateCouponActivityStatus(UpdateCouponActivityStatusRequest request);

        /// <summary>
        /// 活动详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<CouponActivityDetailVo> GetCouponActivityDetail(CouponActivityDetailRequest request);

        /// <summary>
        /// 优惠券发放记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<UserCouponGrantRecordVo>> GetUserCouponGrantRecord(
            UserCouponGrantRecordRequest request);

        /// <summary>
        /// 用户姓名/手机号搜索用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<UserInfoForCouponVo>> SearchUserInfo(SearchUserInfoRequest request);

        /// <summary>
        /// 用户塞券
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> GrantUserCoupon(GrantUserCouponRequest request);

        /// <summary>
        /// 作废优惠券
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> InvalidatedUserCoupon(InvalidatedUserCouponRequest request);

        /// <summary>
        /// 延期优惠券
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> DelayUserCoupon(DelayUserCouponRequest request);

        /// <summary>
        /// 更新发放总数量
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> UpdateActivityTotalNum(UpdateActivityTotalNumRequest request);

        /// <summary>
        /// 开瓶有奖记录查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<DecapAwardDetailVo>> SearchDecapAward(SearchDecapAwardRequest request);

        /// <summary>
        /// 消费赠券活动规则列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<GiftCouponRuleVo>> GetGiftCouponRulePageList(GiftCouponRulePageListRequest request);

        /// <summary>
        /// 消费赠券活动规则详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<GiftCouponRuleDetailVo> GetGiftCouponRuleDetail(GiftCouponRuleDetailRequest request);

        /// <summary>
        /// 新增赠券规则
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> AddGiftCouponRule(AddGiftCouponRuleRequest request);

        /// <summary>
        /// 编辑赠券规则
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> EditGiftCouponRule(EditGiftCouponRuleRequest request);
    }
}
