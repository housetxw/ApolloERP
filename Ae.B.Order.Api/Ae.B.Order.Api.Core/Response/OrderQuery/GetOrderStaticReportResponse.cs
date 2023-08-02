using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Order.Api.Core.Response.OrderQuery
{
    public class GetOrderStaticReportResponse
    {
        /// <summary>
        /// 时间周期
        /// </summary>
        public string CycleTime { get; set; }
        /// <summary>
        /// 订单数量
        /// </summary>
        public int HaveFinishOrderNum { get; set; }

        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal HaveFinishOrderAmount { get; set; }

        /// <summary>
        /// 订单数量
        /// </summary>
        public int NotFinishOrderNum { get; set; }

        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal NotFinishOrderAmount { get; set; }

    }

   
}
