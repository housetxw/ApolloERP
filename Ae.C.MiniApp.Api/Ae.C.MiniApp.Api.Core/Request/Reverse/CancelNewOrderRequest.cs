using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request.Reverse
{
    public class CancelNewOrderRequest
    {
        /// <summary>
        /// 订单号（RGC前缀）
        /// </summary>
        public string OrderNo { get; set; } = string.Empty;

        public long ShopId { get; set; }

        public string CreateBy { get; set; }
    }
}
