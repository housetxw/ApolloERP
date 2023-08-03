using System;
using System.Collections.Generic;
using System.Text;
using Ae.ShopOrder.Service.Core.Model;

namespace Ae.ShopOrder.Service.Core.Request
{
    public class UpdateOtherCouponAmountRequest : BaseOrderOperationConditionDTO
    {
        /// <summary>
        /// 优惠金额
        /// </summary>
        public decimal CouponAmount { get; set; }
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
