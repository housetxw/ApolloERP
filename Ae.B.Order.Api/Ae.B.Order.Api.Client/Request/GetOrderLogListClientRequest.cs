using Ae.B.Order.Api.Core.Request;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.B.Order.Api.Client.Request
{
    public class GetOrderLogListClientRequest : BasePageRequest
    {
        /// <summary>
        /// 订单号
        /// </summary>
        [Required(ErrorMessage = "订单不可为空")]
        public string OrderNo { get; set; }
    }
}
