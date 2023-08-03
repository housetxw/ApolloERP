using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Order.Service.Core.Request.Order
{
    /// <summary>
    /// 得到订单的产品请求
    /// </summary>
    public class GetOrderProductRequest: BaseGetRequest
    {
        [Required]
        public List<string> OrderNos { get; set; }

    }
}
