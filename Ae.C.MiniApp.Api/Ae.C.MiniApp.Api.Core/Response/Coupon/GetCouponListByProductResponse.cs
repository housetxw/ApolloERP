using Ae.C.MiniApp.Api.Core.Model.Coupon;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Response.Coupon
{
    /// <summary>
    /// UserCouponListForProduct
    /// </summary>
    public class UserCouponListForProduct
    {
        /// <summary>
        /// 活动Id
        /// </summary>
        public long ActId { get; set; }

        /// <summary>
        /// 券Id
        /// </summary>
        public long CouponRuleId { get; set; }

        /// <summary>
        /// 用户优惠券Id
        /// </summary>
        public long UserCouponId { get; set; }

        /// <summary>
        /// 优惠券规则名称（满减券¥20满0元可用）
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 优惠券显示名称（电池优惠券）
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 分类（0未设置 1优惠券）
        /// </summary>
        public CouponCategory CouponCategory { get; set; }

        /// <summary>
        /// 类型（0未设置 1满减券 2实付券）
        /// </summary>
        public CouponType RuleType { get; set; }

        /// <summary>
        /// 分类（0未设置 1通用券 2线上券 3门店券）
        /// </summary>
        public CouponRangeType RangeType { get; set; }

        /// <summary>
        /// 面值
        /// </summary>
        public decimal Value { get; set; }

        /// <summary>
        /// 使用级别阈值
        /// </summary>
        public decimal Threshold { get; set; }

        /// <summary>
        /// 使用规则描述
        /// </summary>
        public string UseRuleDesc { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; } = DateTime.Now;
    }

    /// <summary>
    /// GetCouponListByProductResponse
    /// </summary>
    public class GetCouponListByProductResponse
    {
        /// <summary>
        /// 可领取的列表
        /// </summary>
        public List<UserCouponListForProduct> CanReceive { get; set; }

        /// <summary>
        ///  已领取的列表
        /// </summary>
        public List<UserCouponListForProduct> HasReceive { get; set; }
    }
}
