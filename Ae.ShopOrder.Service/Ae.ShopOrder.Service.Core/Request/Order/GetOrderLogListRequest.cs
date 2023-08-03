using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Request.Order
{
    public class GetOrderLogListRequest : BasePageRequest
    {
        /// <summary>
        /// 订单号
        /// </summary>
        [Required(ErrorMessage = "订单不可为空")]
        public string OrderNo { get; set; }
    }
}
