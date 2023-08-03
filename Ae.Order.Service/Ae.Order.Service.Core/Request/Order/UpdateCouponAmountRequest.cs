using System;
using System.Collections.Generic;
using System.Text;
using Ae.Order.Service.Core.Model.Order;

namespace Ae.Order.Service.Core.Request.Order
{
    public class UpdateCouponAmountRequest : BaseOrderOperationConditionDTO
    {
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
