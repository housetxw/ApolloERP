using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Request
{
    public class ReserveInitialRequest
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 门店ID
        /// </summary>
        public long ShopId { get; set; }
    }
}
