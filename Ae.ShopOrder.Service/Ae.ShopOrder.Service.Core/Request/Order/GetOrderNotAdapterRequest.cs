using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Request.Order
{
    /// <summary>
    /// 订单不适配请求对象
    /// </summary>
    public class GetOrderNotAdapterRequest
    {
        /// <summary>
        /// 门店ID
        /// </summary>
        [Required]
        public long ShopId { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        [Required]
        public string OrderNo { get; set; }
    }
}
