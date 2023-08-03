using Ae.ShopOrder.Service.Core.Model.Stock;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Request.Stock
{
    public class OrderUseStockNotifyRequest
    {
        /// <summary>
        /// 订单号
        /// </summary>
        [Required(ErrorMessage = "订单号不可为空")]
        public string OrderNo { get; set; }
        /// <summary>
        /// 订单占库存详情集合
        /// </summary>
        public List<OrderUseStockDetailProductDTO> UseStockDetails { get; set; }
    }
}
