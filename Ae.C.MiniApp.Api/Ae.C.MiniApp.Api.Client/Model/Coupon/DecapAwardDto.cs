using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Model.Coupon
{
    public class DecapAwardDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 领取状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 付款状态
        /// </summary>
        public int PayStatus { get; set; }
    }
}
