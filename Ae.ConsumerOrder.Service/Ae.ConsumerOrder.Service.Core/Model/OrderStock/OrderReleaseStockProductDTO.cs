using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ConsumerOrder.Service.Core.Model
{
    /// <summary>
    /// 订单释放库存产品信息
    /// </summary>
    public class OrderReleaseStockProductDTO
    {
        /// <summary>
        /// 订单产品Id（订单占库交互的产品唯一标识）
        /// </summary>
        public long OrderProductId { get; set; }
        /// <summary>
        /// 产品编号
        /// </summary>
        public string ProductId { get; set; }
        /// <summary>
        /// 释放数量
        /// </summary>
        public int ReleaseNumber { get; set; }
    }
}
