using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ConsumerOrder.Service.Core.Model
{
    /// <summary>
    /// 订单占用库存详情产品信息
    /// </summary>
    public class OrderUseStockDetailProductDTO
    {
        /// <summary>
        /// 订单产品Id（订单占库交互的产品唯一标识）
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 产品Id
        /// </summary>
        public string ProductId { get; set; }
        /// <summary>
        /// 商品库存状态（0未占库 1占库中 2已占库 3释放中 4已释放）
        /// </summary>
        public sbyte StockStatus { get; set; }
        /// <summary>
        /// 该产品占用批次信息集合
        /// </summary>
        public List<OrderUseStockDetailBatchDTO> Batches { get; set; }
    }
}
