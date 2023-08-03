using System.ComponentModel;

namespace Rg.Pay.Service.Core.Enums.Pay
{
    public enum PayEnum
    {
    }

    /// <summary>
    /// 支付内部单号类型
    /// </summary>
    public enum PayInnerNoTypeEnum
    {
        /// <summary>
        /// 未设置
        /// </summary>
        [Description("未设置")] None = 0,

        /// <summary>
        /// 订单号
        /// </summary>
        [Description("订单号")] OrderNo = 1,

        /// <summary>
        /// 预约编号
        /// </summary>
        [Description("预约编号")] ReserveNo = 2,

        /// <summary>
        /// 到店编号
        /// </summary>
        [Description("到店编号")] ReceiveNo = 3,

        /// <summary>
        /// 兑换码
        /// </summary>
        [Description("兑换码")] RedeemCode = 4
    }

    /// <summary>
    /// 支付方式类型
    /// </summary>
    public enum PayMethodEnum
    {
        /// <summary>
        /// 未设置
        /// </summary>
        [Description("未设置")]
        None = 0,
        /// <summary>
        /// 在线支付
        /// </summary>
        [Description("在线支付")]
        OnlinePay = 1,
        /// <summary>
        /// 到店付款
        /// </summary>
        [Description("到店付款")]
        PayAtShop = 2
    }

    /// <summary>
    /// 实际支付渠道
    /// </summary>
    public enum PayChannelEnum
    {
        /// <summary>
        /// 未设置
        /// </summary>
        [Description("未设置")]
        None = 0,
        /// <summary>
        /// 微信支付
        /// </summary>
        [Description("微信支付")]
        WxPay = 1,
        /// <summary>
        /// 支付宝
        /// </summary>
        [Description("支付宝")]
        Alipay = 2,
        /// <summary>
        /// 银联支付
        /// </summary>
        [Description("银联支付")]
        UnionPay = 3
    }

    /// <summary>
    /// 支付平台类型
    /// </summary>
    public enum PayPlatformTypeEnum
    {
        /// <summary>
        /// 未设置
        /// </summary>
        [Description("未设置")]
        None = 0,
        /// <summary>
        /// 直连
        /// </summary>
        [Description("直连")]
        DirectConnect = 1,
        /// <summary>
        /// 环迅
        /// </summary>
        [Description("环迅")]
        Ips = 2
    }

    /// <summary>
    /// 支付终端类型
    /// </summary>
    public enum PayTerminalTypeEnum
    {
        /// <summary>
        /// 未设置
        /// </summary>
        [Description("未设置")]
        None = 0,
        /// <summary>
        /// 微信小程序
        /// </summary>
        [Description("微信小程序")]
        ApolloErpWxMiniApp = 1,
        /// <summary>
        /// 门店管家APP
        /// </summary>
        [Description("门店管家APP")]
        ApolloErpApp = 2
    }

    /// <summary>
    /// 支付场景类型
    /// </summary>
    public enum PaySceneTypeEnum
    {
        /// <summary>
        /// 未设置
        /// </summary>
        [Description("未设置")]
        None = 0,
        /// <summary>
        /// 主扫收款码支付
        /// </summary>
        [Description("主扫收款码支付")]
        Native = 1,
        /// <summary>
        /// H5支付
        /// </summary>
        [Description("H5支付")]
        Web = 2,
        /// <summary>
        /// App支付
        /// </summary>
        [Description("App支付")]
        App = 3,
        /// <summary>
        /// 小程序和公众号支付
        /// </summary>
        [Description("小程序和公众号支付")]
        JsApi = 4,
        /// <summary>
        /// 被扫付款码支付
        /// </summary>
        [Description("被扫付款码支付")]
        ScanCode = 5
    }

    /// <summary>
    /// 支付状态
    /// </summary>
    public enum PayStatusEnum
    {
        /// <summary>
        /// 新建
        /// </summary>
        [Description("新建")]
        New = 0,
        /// <summary>
        /// 已调用等待回调中
        /// </summary>
        [Description("已调用等待回调中")]
        Waiting = 1,
        /// <summary>
        /// 成功
        /// </summary>
        [Description("成功")]
        Success = 2,
        /// <summary>
        /// 失败
        /// </summary>
        [Description("失败")]
        Failed = 3,
        /// <summary>
        /// 取消
        /// </summary>
        [Description("取消")]
        Cancel = 4,
        /// <summary>
        /// 关闭
        /// </summary>
        [Description("关闭")]
        Close = 5
    }
}
