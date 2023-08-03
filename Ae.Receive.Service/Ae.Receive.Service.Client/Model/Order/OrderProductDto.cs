using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Client.Model.Order
{
    public class OrderProductDto
    {
        public string ProductId { get; set; }

        public string ProductName { get; set; }

        /// <summary>
        /// 产品显示名称
        /// </summary>
        public string DisplayName { get; set; } = string.Empty;
        /// <summary>
        /// 总金额（指乘以购买数量后）
        /// </summary>
        public decimal TotalAmount { get; set; }

        public int Number { get; set; }

        public string OrderNo { get; set; }

        public decimal Price { get; set; }
        /// <summary>
        /// 总数量（指乘以购买数量后）
        /// </summary>
        public int TotalNumber { get; set; }

        /// <summary>
        /// 单位（个/升/斤等）
        /// </summary>
        public string Unit { get; set; } = string.Empty;

        /// <summary>
        /// 图片路径
        /// </summary>
        public string ImageUrl { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// 所属父级订单套餐产品Id
        /// </summary>
        public long ParentOrderPackagePid { get; set; }
    }
}
