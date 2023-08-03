﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Request.Order
{
    public class OrderPayInstallRequest
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
        public string UpdateBy { get; set; } = string.Empty;

        /// <summary>
        /// 支付单号
        /// </summary>
        public string PayNo { get; set; }
        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime PayTime { get; set; }

        /// <summary>
        /// 支付方式0未设置 1在线支付 2到店付款 3月结
        /// </summary>
        public sbyte PayMethod { get; set; }
        /// <summary>
        /// 支付渠道0未设置 1微信支付 2支付宝 3 美团 4线下 9代付款
        /// </summary>
        public sbyte PayChannel { get; set; }

    }
}
