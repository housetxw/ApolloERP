using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Order.Service.Core.Model.Order
{
    /// <summary>
    /// 订单明细商品信息
    /// </summary>
    public class OrderDetailProductDTO
    {
        /// <summary>
        /// 订单产品Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 产品Id
        /// </summary>
        public string ProductId { get; set; } = string.Empty;
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; } = string.Empty;
        /// <summary>
        /// 产品显示名称
        /// </summary>
        public string DisplayName { get; set; } = string.Empty;
        /// <summary>
        /// 产品描述
        /// </summary>
        public string Description { get; set; } = string.Empty;
        /// <summary>
        ///  产品性质，分为 0 实物产品、1 套餐产品、2 服务产品、3 数字产品
        /// </summary>
        public sbyte ProductAttribute { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        public string Label { get; set; } = string.Empty;
        /// <summary>
        /// 图片路径
        /// </summary>
        public string ImageUrl { get; set; } = string.Empty;
        /// <summary>
        /// 市场价（原价）
        /// </summary>
        public decimal MarketingPrice { get; set; }
        /// <summary>
        /// 售价
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 单位（个/升/斤等）
        /// </summary>
        public string Unit { get; set; } = string.Empty;
        /// <summary>
        /// 数量
        /// </summary>
        public int Number { get; set; }
    }
}
