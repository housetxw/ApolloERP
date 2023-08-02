﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.B.Order.Api.Core.Request.OrderQuery
{
    public class GetOrderCarsRequest
    {
        /// <summary>
        /// 订单IDS
        /// </summary>
        [Required]
        public List<string> OrderNos { get; set; }

        /// <summary>
        /// 订单Id（不需要传输)
        /// </summary>
        public List<long> OrderIds { get; set; }
    }
}
