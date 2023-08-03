using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.ConsumerOrder.Service.Core.Enums
{
    public class CouponEnum { }

    #region 优惠券规则相关Enum

    /// <summary>
    /// 优惠券规则：优惠券分类
    /// </summary>
    public enum CouponCategory
    {
        /// <summary>
        /// 未设置
        /// </summary>
        [Description("未设置")]
        NotSet = 0,

        /// <summary>
        /// 优惠券
        /// </summary>
        [Description("优惠券")]
        ApolloErp = 1
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
    /// 优惠券规则：类型使用范围
    /// </summary>
    public enum CouponRangeType
    {
        /// <summary>
        /// 未设置
        /// </summary>
        [Description("未设置")]
        NotSet = 0,

        /// <summary>
        /// 通用券
        /// </summary>
        [Description("通用券")]
        General = 1,

        /// <summary>
        /// 线上券
        /// </summary>
        [Description("线上券")]
        Online = 2,

        /// <summary>
        /// 门店券
        /// </summary>
        [Description("门店券")]
        Shop = 3
    }

    /// <summary>
    /// 支付方式
    /// </summary>
    public enum PayMethod
    {
        /// <summary>
        /// 未设置
        /// </summary>
        [Description("未设置")]
        NotSet = 0,

        /// <summary>
        /// 线上支付
        /// </summary>
        [Description("线上支付")]
        Online = 1,

        /// <summary>
        /// 到店支付
        /// </summary>
        [Description("到店支付")]
        Shop = 2
    }

    /// <summary>
    /// 优惠券规则：有效期开始类型
    /// </summary>
    public enum ValidStartType
    {
        /// <summary>
        /// 未设置
        /// </summary>
        [Description("未设置")]
        NotSet = 0,

        /// <summary>
        /// 领取当天生效
        /// </summary>
        [Description("领取当天生效")]
        EffectiveToday = 1,

        /// <summary>
        /// 指定开始日期
        /// </summary>
        [Description("指定开始日期")]
        SpecifyStartDate = 2
    }

    /// <summary>
    /// 优惠券规则：有效期结束类型
    /// </summary>
    public enum ValidEndType
    {
        /// <summary>
        /// 未设置
        /// </summary>
        [Description("未设置")]
        NotSet = 0,

        /// <summary>
        /// 持续指定天数
        /// </summary>
        [Description("持续指定天数")]
        ContinuousSpecifiedDays = 1,

        /// <summary>
        /// 指定截止日期
        /// </summary>
        [Description("指定截止日期")]
        SpecifyEndDate = 2
    }


    #endregion 优惠券规则相关Enum

    #region 优惠券活动发放规则相关Enum

    /// <summary>
    /// 优惠券活动：渠道
    /// </summary>
    public enum CouponActivityChannel
    {
        /// <summary>
        /// 未设置
        /// </summary>
        [Description("未设置")]
        NotSet = 0,

        /// <summary>
        /// 小程序
        /// </summary>
        [Description("小程序")]
        MiniApp = 1,

        /// <summary>
        /// Android-App
        /// </summary>
        [Description("Android-App")]
        AndroidApp = 2,

        /// <summary>
        /// iOS-App
        /// </summary>
        [Description("iOS-App")]
        iOSApp = 3,

        /// <summary>
        /// S网站
        /// </summary>
        [Description("S网站")]
        SWebSite = 4,

        /// <summary>
        /// 官网
        /// </summary>
        [Description("官网")]
        WebSite = 5,

    }

    /// <summary>
    /// 优惠券活动：状态
    /// </summary>
    public enum CouponActivityStatue
    {
        /// <summary>
        /// 未发布
        /// </summary>
        [Description("未发布")]
        Unpublished = 0,

        /// <summary>
        /// 可领取
        /// </summary>
        [Description("可领取")]
        CanReceive = 1,

        /// <summary>
        /// 暂停领取
        /// </summary>
        [Description("暂停领取")]
        PauseReceive = 2,

        /// <summary>
        /// 已作废
        /// </summary>
        [Description("已作废")]
        Obsoleted = 3
    }

    /// <summary>
    /// 优惠券活动：需要积分类型
    /// </summary>
    public enum CouponActivityIntegralType
    {
        /// <summary>
        /// 未设置
        /// </summary>
        [Description("未设置")]
        NotSet = 0,

        /// <summary>
        /// 鸡蛋
        /// </summary>
        [Description("鸡蛋")]
        GooseFeather = 1,

        /// <summary>
        /// 鹅蛋
        /// </summary>
        [Description("鹅蛋")]
        GooseEgg = 2

    }


    #endregion 优惠券活动发放规则相关Enum

    #region 用户优惠券相关Enum

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
        /// 系统自动触发
        /// </summary>
        [Description("系统自动触发")]
        System = 1,

        /// <summary>
        /// 运营手动触发
        /// </summary>
        [Description("运营手动触发")]
        Manual = 2,

        /// <summary>
        /// 用户自主领取
        /// </summary>
        [Description("用户自主领取")]
        Initiative = 3,

        /// <summary>
        /// 用户积分兑换
        /// </summary>
        [Description("用户积分兑换")]
        Integral = 4,

        /// <summary>
        /// 用户优惠码兑换
        /// </summary>
        [Description("用户优惠码兑换")]
        PromotionCode = 5
    }

    /// <summary>
    /// 用户优惠券：使用状态
    /// </summary>
    public enum UserCouponStatus
    {
        /// <summary>
        /// 已过期
        /// </summary>
        [Description("已过期")]
        Unused = 0,

        /// <summary>
        /// 已使用
        /// </summary>
        [Description("已使用")]
        Used = 1,

        /// <summary>
        /// 已过期
        /// </summary>
        [Description("已过期")]
        Expired = 2
    }

    #endregion 用户优惠券相关Enum

}
