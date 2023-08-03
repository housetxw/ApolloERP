using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Model
{
    public class ReserveProductDTO
    {
        /// <summary>
        ///  产品编码
        /// </summary>
        public string ProductCode { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 图片路径
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// 单位（个/升/斤等）
        /// </summary>
        public string Unit { get; set; } = string.Empty;
        /// <summary>
        /// 销售单价
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 产品总价
        /// </summary>
        public decimal TotalAmount { get; set; }
        /// <summary>
        /// 促销活动ID
        /// </summary>
        public string ActivityId { get; set; } = string.Empty;
    }
}
