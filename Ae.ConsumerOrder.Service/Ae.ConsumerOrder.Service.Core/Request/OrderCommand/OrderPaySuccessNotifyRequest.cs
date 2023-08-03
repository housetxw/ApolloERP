using Ae.ConsumerOrder.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ConsumerOrder.Service.Core.Request
{
    public class OrderPaySuccessNotifyRequest : BaseOrderOperationConditionDTO
    {
        /// <summary>
        /// 支付单号
        /// </summary>
        public string PayNo { get; set; }
        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime PayTime { get; set; }
        /// <summary>
        /// 支付金额
        /// </summary>
        public decimal PayAmount { get; set; }
        /// <summary>
        /// 支付方式
        /// </summary>
        public sbyte PayMethod { get; set; }
        /// <summary>
        /// 支付渠道
        /// </summary>
        public sbyte PayChannel { get; set; }
    }
}
