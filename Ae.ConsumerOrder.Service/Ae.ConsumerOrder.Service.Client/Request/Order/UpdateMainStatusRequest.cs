using Ae.ConsumerOrder.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ConsumerOrder.Service.Client.Request
{
    public class UpdateOrderMainStatusRequest : BaseOrderOperationConditionDTO
    {
        /// <summary>
        /// 订单状态
        /// </summary>
        public sbyte OrderStatus { get; set; }
    }
}
