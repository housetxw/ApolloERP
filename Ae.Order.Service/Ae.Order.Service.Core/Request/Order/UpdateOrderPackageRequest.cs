using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Order.Service.Core.Request.Order
{
    public class UpdateOrderPackageRequest
    {
        /// <summary>
        /// 核销码（RGHX*****）
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string SourceOrderNo { get; set; }

        /// <summary>
        /// 截止有效时间
        /// </summary>
        public string ValidateEnd { get; set; }

        public sbyte Status { get; set; } = 0;

        public string UpdateBy { get; set; }
    }
}
