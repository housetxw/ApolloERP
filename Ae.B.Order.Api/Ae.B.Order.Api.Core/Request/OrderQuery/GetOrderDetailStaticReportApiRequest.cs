using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Order.Api.Core.Request.OrderQuery
{
    public class GetOrderDetailStaticReportApiRequest:BasePageRequest
    {
        /// <summary>
        /// 时间周期（Week,Month,Year)
        /// </summary>
        public string Type { get; set; }

        


    }
}
