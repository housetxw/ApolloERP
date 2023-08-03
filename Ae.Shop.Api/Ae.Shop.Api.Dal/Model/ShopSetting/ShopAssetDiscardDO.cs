using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;

namespace Ae.Shop.Api.Dal.Model
{
    [Table("shop_asset_discard")]
    public class ShopAssetDiscardDO
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 资产ID
        /// </summary>
        [Column("asset_id")]
        public long AssetId { get; set; }
        /// <summary>
        /// 报废类型（0未设置 1自然报废 2灾害事故 3人为损坏）
        /// </summary>
        [Column("discard_type")]
        public sbyte DiscardType { get; set; }
        /// <summary>
        /// 报废原因
        /// </summary>
        [Column("reason")]
        public string Reason { get; set; } = string.Empty;
        /// <summary>
        /// 是否删除 0否 1是
        /// </summary>
        [Column("is_deleted")]
        public sbyte IsDeleted { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [Column("create_by")]
        [NotUpdate]
        public string CreateBy { get; set; } = string.Empty;
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("create_time")]
        [NotUpdate]
        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 修改人
        /// </summary>
        [Column("update_by")]
        public string UpdateBy { get; set; } = string.Empty;
        /// <summary>
        /// 修改时间
        /// </summary>
        [Column("update_time")]
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);
    }
}