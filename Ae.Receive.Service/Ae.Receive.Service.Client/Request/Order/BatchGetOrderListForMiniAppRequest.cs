using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Client.Request.Order
{
    public class BatchGetOrderListForMiniAppRequest
    {
        /// <summary>
        /// 订单号集合
        /// </summary>
        public List<string> OrderNos { get; set; }
        /// <summary>
        /// 小程序类型（0未设置 1C端小程序 2B端小程序）
        /// </summary>
        public sbyte MiniAppType { get; set; }
    }
}
