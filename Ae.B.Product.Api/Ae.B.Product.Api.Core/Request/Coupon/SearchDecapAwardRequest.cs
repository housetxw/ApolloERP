using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Request.Coupon
{
    /// <summary>
    /// SearchDecapAwardRequest
    /// </summary>
    public class SearchDecapAwardRequest : BasePageRequest
    {
        /// <summary>
        /// 条码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 领取状态
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 打款状态
        /// </summary>
        public int? PayStatus { get; set; }

        /// <summary>
        /// 付款渠道
        /// </summary>
        public string PayChannel { get; set; }
    }
}
