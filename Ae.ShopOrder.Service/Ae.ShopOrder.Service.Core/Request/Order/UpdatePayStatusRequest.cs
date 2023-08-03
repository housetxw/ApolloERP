using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Request.Order
{
    public class UpdatePayStatusRequest
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
        /// 支付单号
        /// </summary>
        public string PayNo { get; set; }
        /// <summary>
        /// 支付状态
        /// </summary>
        public sbyte PayStatus { get; set; }
        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime PayTime { get; set; }
        /// <summary>
        /// 支付金额
        /// </summary>
        public decimal PayAmount { get; set; }
        /// <summary>
        /// 支付方式
        /// </summary>
        public sbyte PayMethod { get; set; }
        /// <summary>
        /// 支付渠道
        /// </summary>
        public sbyte PayChannel { get; set; }
    }
}
