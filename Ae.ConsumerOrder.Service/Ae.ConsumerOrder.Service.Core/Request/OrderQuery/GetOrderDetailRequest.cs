﻿using Ae.ConsumerOrder.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.ConsumerOrder.Service.Core.Request
{
    /// <summary>
    /// 获取订单详情请求参数
    /// </summary>
    public class GetOrderDetailRequest : BaseUserRequest
    {
        /// <summary>
        /// 订单号
        /// </summary>
        [Required(ErrorMessage = "订单号不能为空")]
        public string OrderNo { get; set; }
    }
}
