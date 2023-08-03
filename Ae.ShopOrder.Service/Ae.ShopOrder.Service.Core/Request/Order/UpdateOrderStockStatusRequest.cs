using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Model.Order
{
   public  class UpdateOrderStockStatusRequest
    {
        /// <summary>
        /// 订单号
        /// </summary>
        [Required(ErrorMessage = "订单号不能为空")]
        public string OrderNo { get; set; }
        /// <summary>
        /// 操作修改人
        /// </summary>
        [Required(ErrorMessage = "操作修改人不能为空")]
        public string UpdateBy { get; set; } = string.Empty;

        /// <summary>
        /// 库存状态
        /// </summary>
        public sbyte StockStatus { get; set; }
    }
}
