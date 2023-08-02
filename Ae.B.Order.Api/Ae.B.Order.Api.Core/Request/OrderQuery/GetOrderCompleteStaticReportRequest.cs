using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Order.Api.Core.Request.OrderQuery
{
    public class GetOrderCompleteStaticReportRequest
    {
        /// <summary>
        /// 开始日期
        /// </summary>
        public string StartTime { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public string EndTime { get; set; }
    }
}
