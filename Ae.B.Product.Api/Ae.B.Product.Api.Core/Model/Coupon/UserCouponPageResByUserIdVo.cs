using Newtonsoft.Json;
using Ae.B.Product.Api.Common.Format;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.B.Product.Api.Core.Model.Coupon
{
    /// <summary>
    /// 
    /// </summary>
    public class UserCouponPageResByUserIdVo
    {
        /// <summary>
        /// 自增主键
        /// </summary>
        public long UserCouponId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 活动Id
        /// </summary>
        public long CouponActivityId { get; set; }

        /// <summary>
        /// 规则Id
        /// </summary>
        public long CouponRuleId { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImageUrl { get; set; } = string.Empty;

        /// <summary>
        /// 面值
        /// </summary>
        public decimal Value { get; set; }

        /// <summary>
        /// 优惠券规则名称（满减券¥20满0元可用）
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 优惠券显示名称（优惠券）
        /// </summary>
        public string DisplayName { get; set; } = string.Empty;

        /// <summary>
        /// 使用规则描述
        /// </summary>
        public string UseRuleDesc { get; set; } = string.Empty;

        /// <summary>
        /// 类型（0未设置 1满减券 2实付券）
        /// </summary>
        public CouponType Type { get; set; }

        /// <summary>
        /// 使用状态（0未使用 1已使用 2已过期）
        /// </summary>
        public UserCouponStatus Status { get; set; }

        /// <summary>
        /// ！！！仅当UserCouponStatus(Status)为0时，此字段才有用！！！
        /// </summary>
        public bool OrderEnabled { get; set; }

        /// <summary>
        /// 开始有效时间
        /// </summary>
        [JsonConverter(typeof(FmtDTToDateWithPeriod))]
        public DateTime StartValidTime { get; set; }

        /// <summary>
        /// 截止有效时间
        /// </summary>
        [JsonConverter(typeof(FmtDTToDateWithPeriod))]
        public DateTime EndValidTime { get; set; }
    }

    /// <summary>
    /// 优惠券规则：优惠券类型
    /// </summary>
    public enum CouponType
    {
        /// <summary>
        /// 未设置
        /// </summary>
        [Description("未设置")]
        NotSet = 0,

        /// <summary>
        /// 满减券
        /// </summary>
        [Description("满减券")]
        FullCut = 1,

        /// <summary>
        /// 实付券
        /// </summary>
        [Description("实付券")]
        RealPayVoucher = 2
    }

    /// <summary>
    /// 用户优惠券：使用状态
    /// </summary>
    public enum UserCouponStatus
    {
        /// <summary>
        /// 未使用
        /// </summary>
        [Description("未使用")] Unused = 0,

        /// <summary>
        /// 已使用
        /// </summary>
        [Description("已使用")] Used = 1,

        /// <summary>
        /// 已过期
        /// </summary>
        [Description("已过期")] Expired = 2,

        /// <summary>
        /// 已作废
        /// </summary>
        [Description("已作废")] Invalidated = 3
    }

    /// <summary>
    /// 用户优惠券：发放方式
    /// </summary>
    public enum UserCouponGrantMethod
    {
        /// <summary>
        /// 未设置
        /// </summary>
        [Description("未设置")]
        NotSet = 0,

        /// <summary>
        /// 新用户注册发放
        /// </summary>
        [Description("新用户注册发放")]
        NewRegisterUser = 1,

        /// <summary>
        /// 系统自动触发
        /// </summary>
        [Description("系统自动触发")]
        System = 2,

        /// <summary>
        /// 运营手动触发
        /// </summary>
        [Description("运营手动触发")]
        Manual = 3,

        /// <summary>
        /// 用户自主领取
        /// </summary>
        [Description("用户自主领取")]
        Initiative = 4,

        /// <summary>
        /// 用户积分兑换
        /// </summary>
        [Description("用户积分兑换")]
        Integral = 5,

        /// <summary>
        /// 用户优惠码兑换
        /// </summary>
        [Description("用户优惠码兑换")]
        PromotionCode = 6,

        /// <summary>
        /// 用户购买
        /// </summary>
        [Description("用户购买")]
        Purchase = 7,
        [Description("门店发放")]
        GrantShop = 8,

    }
}
