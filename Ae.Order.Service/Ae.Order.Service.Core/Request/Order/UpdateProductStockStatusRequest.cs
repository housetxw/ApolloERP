using Ae.Order.Service.Core.Model.Order;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Order.Service.Core.Request.Order
{
    public class UpdateProductStockStatusRequest : BaseOrderOperationConditionDTO
    {
        /// <summary>
        /// 库存状态
        /// </summary>
        public sbyte StockStatus { get; set; }
        /// <summary>
        /// 订单商品ID
        /// </summary>
        public List<long> OrderPids { get; set; }
    }
}
