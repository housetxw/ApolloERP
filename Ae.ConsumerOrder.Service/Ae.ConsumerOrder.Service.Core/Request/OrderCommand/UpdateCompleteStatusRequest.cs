using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ConsumerOrder.Service.Core.Request.OrderCommand
{
    public class UpdateCompleteStatusRequest
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 操作者
        /// </summary>
        public string UpdateBy { get; set; }
    }
}
