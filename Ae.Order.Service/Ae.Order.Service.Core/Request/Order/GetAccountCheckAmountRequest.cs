using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Order.Service.Core.Request.Order
{
    /// <summary>
    /// 对账金额请求对象
    /// </summary>
    public class GetAccountCheckAmountRequest
    {
        /// <summary>
        /// 订单号
        /// </summary>
        [Required]
        public  List<string> OrderNos { get; set; }
    }
}
