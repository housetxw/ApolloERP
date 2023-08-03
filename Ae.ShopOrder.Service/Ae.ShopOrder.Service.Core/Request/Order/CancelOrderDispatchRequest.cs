using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Request.Order
{
    public class CancelOrderDispatchRequest
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }

        public string CreateBy { get; set; }
    }
}
