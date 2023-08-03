using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Request.Arrival
{
    /// <summary>
    /// 收款请求对象
    /// </summary>
    public class PaySaveRequest
    {
        /// <summary>
        /// 到店记录Id
        /// </summary>
        public string ArrivalId { get; set; }

        /// <summary>
        /// 支付的方式（0未设置 1在线支付 2到店付款）
        /// </summary>
        public int PayMethod { get; set; }

        /// <summary>
        /// 支付的渠道
        /// </summary>
        public int PayChannel { get; set; }

        /// <summary>
        /// 总金额
        /// </summary>
        public decimal SumPrice { get; set; }

        /// <summary>
        /// 操作者
        /// </summary>
        public string UserName { get; set; }
    }
}
