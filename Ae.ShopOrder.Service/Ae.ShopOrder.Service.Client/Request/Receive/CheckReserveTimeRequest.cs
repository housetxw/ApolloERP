using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Client.Request.Receive
{
    public class CheckReserveTimeRequest
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { get; set; }

        public long ShopId { get; set; }

        public string CarId { get; set; }
        /// <summary>
        /// 预约时间
        /// </summary>
        public DateTime ReserveTime { get; set; }
    }
}
