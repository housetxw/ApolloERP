using Ae.ConsumerOrder.Service.Core.Enums.Pay;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ConsumerOrder.Service.Client.Model
{
    public class PayDTO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 支付单号
        /// </summary>
        public string PayNo { get; set; } = string.Empty;
        /// <summary>
        /// 内部单号类型（0未设置 1订单号 2预约编号 3预约编号）
        /// </summary>
        public PayInnerNoTypeEnum InnerNoType { get; set; }
        /// <summary>
        /// 内部单号（根据内部单号类型决定传值）
        /// </summary>
        public string OrderNo { get; set; } = string.Empty;
        /// <summary>
        /// 预支付交易会话标识
        /// </summary>
        public string PrepayId { get; set; } = string.Empty;
        /// <summary>
        /// 外部交易流水单号（微信或支付宝返回的）
        /// </summary>
        public string TradeNo { get; set; } = string.Empty;
        /// <summary>
        /// 外部商户单号（微信或支付宝返回的）
        /// </summary>
        public string MerchantOrderNo { get; set; } = string.Empty;
        /// <summary>
        /// 外部平台交易流水单号（环迅返回的）
        /// </summary>
        public string PlatformTradeNo { get; set; } = string.Empty;
        /// <summary>
        /// 支付方式（0未设置 1在线支付 2到店付款）
        /// </summary>
        public PayMethodEnum Method { get; set; }
        /// <summary>
        /// 实际支付渠道（0未设置 1微信支付 2支付宝 3银联支付）
        /// </summary>
        public PayChannelEnum Channel { get; set; }
        /// <summary>
        /// 支付平台类型（0未设置 1直连 2环迅）
        /// </summary>
        public PayPlatformTypeEnum PlatformType { get; set; }
        /// <summary>
        /// 支付终端类型（0未设置 1小程序 2门店管家App）
        /// </summary>
        public PayTerminalTypeEnum TerminalType { get; set; }
        /// <summary>
        /// 支付场景类型（0未设置 1主扫收款码支付 2H5支付 3App支付 4小程序和公众号支付 5被扫付款码支付）
        /// </summary>
        public PaySceneTypeEnum SceneType { get; set; }
        /// <summary>
        /// 支付状态（0新建 1已调用等待回调中 2成功 3失败 4取消 5关闭）
        /// </summary>
        public PayStatusEnum Status { get; set; }
        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime PayTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 应用ID（或小程序ID）
        /// </summary>
        public string AppId { get; set; } = string.Empty;
        /// <summary>
        /// 卖家收款账号（商户号或支付宝账号）
        /// </summary>
        public string SellerAccount { get; set; } = string.Empty;
        /// <summary>
        /// 买家付款账号（openid或支付宝账号或Code）
        /// </summary>
        public string BuyerAccount { get; set; } = string.Empty;
        /// <summary>
        /// 交易金额（单位为元）
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// 币种（默认CNY）
        /// </summary>
        public string Currency { get; set; } = string.Empty;
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// 删除标识
        /// </summary>
        public sbyte IsDeleted { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 修改人
        /// </summary>
        public string UpdateBy { get; set; } = string.Empty;
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);
    }
}
