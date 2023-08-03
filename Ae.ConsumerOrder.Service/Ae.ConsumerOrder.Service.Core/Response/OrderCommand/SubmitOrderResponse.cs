using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ConsumerOrder.Service.Core.Response
{
    /// <summary>
    /// 提交订单返回参数
    /// </summary>
    public class SubmitOrderResponse
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }
    }
}
