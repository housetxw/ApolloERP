using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Model
{
    public class ReserveListPayDTO
    {
        /// <summary>
        /// 预约ID
        /// </summary>
        public long ReserveId { get; set; }
        /// <summary>
        /// 支付金额
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// 支付按钮
        /// </summary>
        public bool DisplayPayButton { get; set; }
        /// <summary>
        /// 支付状态（0未支付 1已支付）
        /// </summary>
        public sbyte PayStatus { get; set; }
    }
}
