using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Model.Order
{
    public class OrderDetailForBossFinanceInfoDto
    {
        /// <summary>
        /// 支付状态（0未支付 1已支付）
        /// </summary>
        public sbyte PayStatus { get; set; }
        /// <summary>
        /// 支付渠道（0未设置 1微信支付 2支付宝）
        /// </summary>
        public sbyte PayChannel { get; set; }
        /// <summary>
        /// 支付方式（0未设置 1在线支付 2到店付款）
        /// </summary>
        public sbyte PayMethod { get; set; }
        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime PayTime { get; set; } = new DateTime(1900, 1, 1);

        /// <summary>
        /// 支付单号
        /// </summary>
        public string PayNo { get; set; } = string.Empty;
        /// <summary>
        /// 外部交易流水号
        /// </summary>
        public string TradeNo { get; set; } = string.Empty;
        /// <summary>
        /// 买家付款账号（openid或支付宝账号）
        /// </summary>
        public string BuyerAccount { get; set; } = string.Empty;

        /// <summary>
        /// 退款状态（0未退款 1已退款）
        /// </summary>
        public sbyte RefundStatus { get; set; }
        /// <summary>
        /// 到账状态（0未到账 1已到账）
        /// </summary>
        public sbyte MoneyArriveStatus { get; set; }
        /// <summary>
        /// 是否发生过逆向申请（0否 1是）
        /// </summary>
        public sbyte IsOccurReverse { get; set; }
        /// <summary>
        /// 逆向申请单状态（10已提交 20已确认 30已完成 40已取消）
        /// </summary>
        public sbyte ReverseStatus { get; set; }
        /// <summary>
        /// 退款时间
        /// </summary>
        public DateTime RefundTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 退款金额
        /// </summary>
        public decimal RefundAmount { get; set; }
    }
}
