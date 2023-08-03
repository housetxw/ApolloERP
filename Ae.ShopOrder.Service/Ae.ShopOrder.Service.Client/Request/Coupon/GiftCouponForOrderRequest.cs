using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.ShopOrder.Service.Client.Request.Coupon
{
    /// <summary>
    /// GiftCouponForOrderRequest
    /// </summary>
    public class GiftCouponForOrderRequest
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        [Required(ErrorMessage = "用户id不能为空")]
        public string UserId { get; set; }

        /// <summary>
        /// 渠道：
        /// </summary>
        public AdaptChannelEnum Channel { get; set; }

        /// <summary>
        /// 终端类型
        /// </summary>
        public CouponActivityChannel TerminalType { get; set; }

        /// <summary>
        /// 产品列表
        /// </summary>
        public List<OrderProductRequest> ProductList { get; set; }
    }

    /// <summary>
    /// OrderProductRequest
    /// </summary>
    public class OrderProductRequest
    {
        /// <summary>
        /// 产品pid
        /// </summary>
        public string Pid { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 最小类目
        /// </summary>
        public string CategoryCode { get; set; }
    }

    public enum AdaptChannelEnum
    {
        /// <summary>
        /// 未设置
        /// </summary>
        [Description("未设置")] NoSet = 0,

        /// <summary>
        /// 线下
        /// </summary>
        [Description("线下渠道")] OffLine = 1,

        /// <summary>
        /// 线上
        /// </summary>
        [Description("线上渠道")] OnLine = 2
    }

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
}
