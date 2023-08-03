using Ae.Shop.Api.Client.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Client.Request
{
   public class OrderUseStockNotifyClientRequest
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 订单占库存详情集合
        /// </summary>
        public List<OrderUseStockDetailProductDTO> UseStockDetails { get; set; }
    }
}
