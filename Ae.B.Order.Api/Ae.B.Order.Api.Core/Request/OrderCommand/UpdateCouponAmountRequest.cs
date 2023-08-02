using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Ae.B.Order.Api.Core.Request.OrderCommand
{
    public class UpdateCouponAmountRequest
    {
        /// <summary>
        /// 订单号
        /// </summary>
        [Required(ErrorMessage = "订单号不能为空")]
        public string OrderNo { get; set; }
        /// <summary>
        /// 操作修改人
        /// </summary>
        public string UpdateBy { get; set; }
        /// <summary>
        /// 优惠金额
        /// </summary>
        public decimal TotalCouponAmount { get; set; }
        /// <summary>
        /// 实付金额
        /// </summary>
        public decimal ActualAmount { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = string.Empty;
    }
}
