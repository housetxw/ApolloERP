using Ae.ShopOrder.Service.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Request.Order
{
    public class OrderReverseNotifyRequest
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
        /// 逆向申请单号
        /// </summary>
        public string ReverseNo { get; set; }
        /// <summary>
        /// 逆向申请类型
        /// </summary>
        public ReverseApplyTypeEnum ApplyType { get; set; }
        /// <summary>
        /// 逆向申请单状态
        /// </summary>
        public ReverseStatusEnum ReverseStatus { get; set; }
        /// <summary>
        /// 退款状态（0未退款 1已退款）
        /// </summary>
        public sbyte RefundStatus { get; set; }
    }
}
