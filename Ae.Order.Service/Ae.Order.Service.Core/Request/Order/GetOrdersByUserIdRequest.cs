using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Order.Service.Core.Request.Order
{
    public class GetOrdersByUserIdRequest
    {
        public string UserId { get; set; }

        public long ShopId { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime BeginOrderTime { get; set; } = new DateTime(1900, 1, 1);
    }
}
