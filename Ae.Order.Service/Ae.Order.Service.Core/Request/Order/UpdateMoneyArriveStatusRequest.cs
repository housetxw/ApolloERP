using Ae.Order.Service.Core.Model.Order;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Order.Service.Core.Request.Order
{
    public class UpdateMoneyArriveStatusRequest 
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
        public string UpdateBy { get; set; }

        /// <summary>
        /// 支付到账状态
        /// </summary>
        public sbyte MoneyArriveStatus { get; set; }
    }
}
