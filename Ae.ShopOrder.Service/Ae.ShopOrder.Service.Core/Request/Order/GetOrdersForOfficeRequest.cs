using Ae.ShopOrder.Service.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Request.Order
{
    public class GetOrdersForOfficeRequest:BasePageRequest
    {
        public long ShopId { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        [Required(ErrorMessage = "业务状态不能为空")]
        public OrderStatusForOfficeEnum OrderBusinessStatus { get; set; }
    }
}
