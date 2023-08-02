using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Model.Coupon
{
    public class DecapAwardDetailDto
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

        /// <summary>
        /// 领取时间
        /// </summary>
        public DateTime? DrawTime { get; set; }

        /// <summary>
        /// 领取人OpenId
        /// </summary>
        public string DrawOpenId { get; set; }

        /// <summary>
        /// 领取人UserId
        /// </summary>
        public string DrawUserId { get; set; }

        /// <summary>
        /// 打款时间
        /// </summary>
        public DateTime? PayTime { get; set; }

        /// <summary>
        /// 付款渠道
        /// </summary>
        public string ClientChannel { get; set; }
    }
}
