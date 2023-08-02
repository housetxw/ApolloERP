using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Order.Api.Core.Response.OrderCommand
{
    /// <summary>
    /// 订单提交返回对象
    /// </summary>
    public class SubmitOrderResponse
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }
    }
}
