using Ae.ConsumerOrder.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ConsumerOrder.Service.Client.Request
{
    public class UpdateProductTotalCostPriceRequest : BaseOrderOperationConditionDTO
    {
        /// <summary>
        /// 订单商品ID
        /// </summary>
        public long OrderPid { get; set; }
        /// <summary>
        /// 总成本
        /// </summary>
        public decimal TotalCostPrice { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
