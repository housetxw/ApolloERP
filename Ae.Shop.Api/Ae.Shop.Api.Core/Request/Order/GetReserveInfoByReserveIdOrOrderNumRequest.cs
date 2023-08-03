using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Request.Order
{
    public class GetReserveInfoByReserveIdOrOrderNum
    {
        /// <summary>
        /// 预约ID
        /// </summary>
        public List<long> ReserveIds { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public List<string> OrderNumbers { get; set; }
    }
}
