using Ae.Receive.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Response
{
    public class CanReserveOrderListResponse
    {
        /// <summary>
        /// 可预约订单
        /// </summary>
        public List<CanReserveOrderDTO> ReserveOrders { get; set; }
        /// <summary>
        /// 总数量
        /// </summary>
        public int TotalItems { get; set; }
    }
}
