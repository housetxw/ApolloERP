using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Model
{
    public class ShopAssetMaintainVO
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 资产ID
        /// </summary>
        public long AssetId { get; set; }
        /// <summary>
        /// 维修日期
        /// </summary>
        public DateTime Date { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 维修费用
        /// </summary>
        public decimal Cost { get; set; }
        /// <summary>
        /// 供应商
        /// </summary>
        public string Supplier { get; set; } = string.Empty;
        /// <summary>
        /// 维修内容
        /// </summary>
        public string Content { get; set; } = string.Empty;
        /// <summary>
        /// 是否删除 0否 1是
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
    }
}
