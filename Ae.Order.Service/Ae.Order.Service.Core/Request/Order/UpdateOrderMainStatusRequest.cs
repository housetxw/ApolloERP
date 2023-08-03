using Ae.Order.Service.Core.Model.Order;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Order.Service.Core.Request.Order
{
    public class UpdateOrderMainStatusRequest : BaseOrderOperationConditionDTO
    {
        /// <summary>
        /// 订单状态
        /// </summary>
        public sbyte OrderStatus { get; set; }
    }
}
