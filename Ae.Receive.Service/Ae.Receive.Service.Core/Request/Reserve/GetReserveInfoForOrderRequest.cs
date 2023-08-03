using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Request.Reserve
{
    public class GetReserveInfoForOrderRequest
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { get; set; }
    }
}
