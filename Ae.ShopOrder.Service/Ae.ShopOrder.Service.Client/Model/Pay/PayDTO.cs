using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Client.Model.Pay
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
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; } = string.Empty;
        /// <summary>
        /// 外部交易流水号
        /// </summary>
        public string TradeNo { get; set; } = string.Empty;

        public string PlatformTradeNo { get; set; } = string.Empty;
        /// <summary>
        /// 支付方式（0未设置 1在线支付 2到店付款）
        /// </summary>
        public sbyte Method { get; set; }
        /// <summary>
        /// 支付渠道（0未设置 1微信支付 2支付宝）
        /// </summary>
        public sbyte Channel { get; set; }
        /// <summary>
        /// 支付状态（0新建 1已调用等待回调中 2成功 3失败 4关闭 5取消）
        /// </summary>
        public sbyte Status { get; set; }
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
        /// 买家付款账号（openid或支付宝账号）
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
