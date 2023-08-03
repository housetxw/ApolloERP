﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Client.Model
{
    /// <summary>
    /// 订单明细商品信息
    /// </summary>
    public class UseStockOrderDetailProductDTO
    {
        /// <summary>
        /// 订单产品Id（订单占库交互的产品唯一标识）
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 产品编号
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
        /// 0 实物产品、1 套餐产品、2 服务产品、3 数字产品
        /// </summary>
        public sbyte ProductAttribute { get; set; }
        /// <summary>
        /// 是否是服务产品
        /// </summary>
        public sbyte IsServiceProduct { get; set; }
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
        /// 单个套餐数量
        /// </summary>
        public int Number { get; set; }

        public string OrderNo { get; set; }
        public int TotalNumber { get; set; }

    }

}
