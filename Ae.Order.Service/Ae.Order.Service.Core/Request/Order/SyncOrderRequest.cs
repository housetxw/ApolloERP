using Ae.Order.Service.Core.Model.Order;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Order.Service.Core.Request.Order
{
    public class SyncOrderRequest
    {
        /// <summary>
        /// 订单
        /// </summary>
        public OrderDTO Order { get; set; }
        /// <summary>
        /// 商品
        /// </summary>
        public List<OrderProductDTO> OrderProducts { get; set; }
    }
}
