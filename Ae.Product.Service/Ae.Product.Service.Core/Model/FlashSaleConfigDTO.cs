using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Core.Model
{
    public class FlashSaleConfigDTO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 有效开始时间
        /// </summary>
        public DateTime ActiveStartTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 有效结束时间
        /// </summary>
        public DateTime ActiveEndTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; } = string.Empty;
        /// <summary>
        /// 产品编号
        /// </summary>
        public string ProductId { get; set; } = string.Empty;
        /// <summary>
        /// 数量
        /// </summary>
        public int Num { get; set; }

        /// <summary>
        /// 已售数量
        /// </summary>
        public int SaleNum { get; set; } = 0;

        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }

        public decimal SalesPrice { get; set; }

        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// 状态(待审核,已生效,已结束,已取消)
        /// </summary>
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// 删除标识
        /// </summary>
        public sbyte IsDeleted { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 修改人
        /// </summary>
        public string UpdateBy { get; set; } = string.Empty;
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);

        public string Image1 { get; set; }
    }
}
