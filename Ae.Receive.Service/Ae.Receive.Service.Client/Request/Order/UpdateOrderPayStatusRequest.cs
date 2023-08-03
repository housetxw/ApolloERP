using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Client.Request.Order
{
    public class UpdateOrderPayStatusRequest
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public List<string> OrderNo { get; set; }
        /// <summary>
        /// 支付的方式（0未设置 1在线支付 2到店付款）
        /// </summary>
        public int PayMethod { get; set; }

        /// <summary>
        /// 支付的渠道
        /// </summary>
        public int PayChannel { get; set; }

        /// <summary>
        /// 操作者
        /// </summary>
        public string UpdateBy { get; set; }
    }
}
