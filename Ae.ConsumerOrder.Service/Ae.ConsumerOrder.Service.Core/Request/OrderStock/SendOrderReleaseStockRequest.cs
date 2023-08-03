using Ae.ConsumerOrder.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ConsumerOrder.Service.Core.Request
{
    public class SendOrderReleaseStockRequest
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 释放库存的产品信息集合
        /// </summary>
        public List<OrderReleaseStockProductDTO> ReleaseStockProducts { get; set; }
    }
}
