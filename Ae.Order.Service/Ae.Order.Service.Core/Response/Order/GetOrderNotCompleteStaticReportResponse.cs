using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Order.Service.Core.Response.Order
{
    public class GetOrderNotCompleteStaticReportResponse
    {
        /// <summary>
        /// 订单数量
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal OrderAmount { get; set; }
    }
}
