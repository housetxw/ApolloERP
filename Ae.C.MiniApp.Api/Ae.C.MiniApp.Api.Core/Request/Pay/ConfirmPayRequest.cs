using Rg.Pay.Service.Core.Enums.Pay;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request
{
    public class ConfirmPayRequest
    {
        /// <summary>
        /// 内部单号类型
        /// </summary>
        public PayInnerNoTypeEnum InnerNoType { get; set; }
        /// <summary>
        /// 内部单号（根据内部单号类型决定传值）
        /// </summary>
        public string OrderNo { get; set; } = string.Empty;
        /// <summary>
        /// 买家付款账号（openid或支付宝账号或Code）
        /// </summary>
        public string BuyerAccount { get; set; } = string.Empty;
        /// <summary>
        /// 交易金额（单位为元）
        /// </summary>
        public decimal Amount { get; set; }
    }
}
