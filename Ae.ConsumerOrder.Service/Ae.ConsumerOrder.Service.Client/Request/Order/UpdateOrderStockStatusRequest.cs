using Ae.ConsumerOrder.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ConsumerOrder.Service.Client.Request
{
    public class UpdateOrderStockStatusRequest : BaseOrderOperationConditionDTO
    {
        /// <summary>
        /// 库存状态
        /// </summary>
        public sbyte StockStatus { get; set; }
    }
}
