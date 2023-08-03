using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.FMS.Service.Client.Request
{
   public class GetOrderByNoClientRequest
    {
        /// <summary>
        /// 订单号
        /// </summary>
        [Required(ErrorMessage = "必须指定订单号")]
        public string OrderNo { get; set; }
    }
}
