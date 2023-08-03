using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Model.Stock
{
    /// <summary>
    /// 订单占用库存详情批次信息
    /// </summary>
    public class OrderUseStockDetailBatchDTO
    {
        /// <summary>
        /// 批次号
        /// </summary>
        public string BatchNo { get; set; }
        /// <summary>
        /// 占用数量
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// 成本（单）价
        /// </summary>
        public decimal CostPrice { get; set; }
    }
}
