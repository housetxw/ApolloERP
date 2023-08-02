using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Order.Api.Core.Response.OrderQuery
{
    public class GetOrderCompleteStaticReportResponse
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
