using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Order.Api.Core.Request.OrderCommand
{
    public class UpdateOrderPackageRequest
    {
        public string Code { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string SourceOrderNo { get; set; }

        public string ValidateEnd { get; set; }
        public sbyte Status { get; set; } = 0;

        public string UpdateBy { get; set; }
    }
}
