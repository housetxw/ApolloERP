using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Product.Service.Core.Request.Promotion
{
    /// <summary>
    /// 根据订单查询活动
    /// </summary>
    public class ProductActivityByOrderNosRequest
    {
        /// <summary>
        /// 订单号
        /// </summary>
        [Required(ErrorMessage = "订单号不能为空")]
        public List<string> OrderNos { get; set; }
    }
}
