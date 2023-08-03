using Ae.ShopOrder.Service.Client.Model.Order;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Client.Request.Order
{
    public class SyncOrderRequest
    {
        /// <summary>
        /// 订单
        /// </summary>
        public MainOrderDTO Order { get; set; }
        /// <summary>
        /// 商品
        /// </summary>
        public List<MainOrderProductDTO> OrderProducts { get; set; }
    }
}
