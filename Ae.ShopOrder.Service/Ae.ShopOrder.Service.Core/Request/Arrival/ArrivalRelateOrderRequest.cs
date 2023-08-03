using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Request.Arrival
{
    public class ArrivalRelateOrderRequest
    {
        /// <summary>
        /// 到店记录Id
        /// </summary>
        public long ArrivalId { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { get; set; }
        public string CreateBy { get; set; }
    }
}
