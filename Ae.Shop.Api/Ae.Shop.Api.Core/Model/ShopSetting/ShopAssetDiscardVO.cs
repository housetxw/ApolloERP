using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Model
{
    public class ShopAssetDiscardVO
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
        /// 报废类型（0未设置 1自然报废 2灾害事故 3人为损坏）
        /// </summary>
        public sbyte DiscardType { get; set; }
        /// <summary>
        /// 报废原因
        /// </summary>
        public string Reason { get; set; } = string.Empty;
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
