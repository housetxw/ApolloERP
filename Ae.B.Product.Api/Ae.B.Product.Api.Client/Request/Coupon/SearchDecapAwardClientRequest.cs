using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Request.Coupon
{
    public class SearchDecapAwardClientRequest
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

        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; } = 10;
    }
}
