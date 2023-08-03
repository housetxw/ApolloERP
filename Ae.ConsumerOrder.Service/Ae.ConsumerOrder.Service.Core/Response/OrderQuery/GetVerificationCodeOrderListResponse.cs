using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ConsumerOrder.Service.Core.Response
{
    public class GetVerificationCodeOrderListResponse
    {
        /// <summary>
        /// 核销生成的订单号
        /// </summary>
        public string VerifyOrderNo { get; set; } = string.Empty;
        /// <summary>
        /// 核销码（RGHX*****）
        /// </summary>
        public string Code { get; set; } = string.Empty;
        /// <summary>
        /// 核销时间
        /// </summary>
        public DateTime VerifyTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 核销服务商品名称
        /// </summary>
        public string ProductName { get; set; } = string.Empty;
    }
}
