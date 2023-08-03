using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Order.Service.Core.Request.Order
{
    public class CancelOrderRequest
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }

        public int ShopId { get; set; }

        public string CreateBy { get; set; }
    }
}
