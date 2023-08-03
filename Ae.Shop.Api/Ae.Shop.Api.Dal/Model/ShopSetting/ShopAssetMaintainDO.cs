using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;

namespace Ae.Shop.Api.Dal.Model
{
    [Table("shop_asset_maintain")]
    public class ShopAssetMaintainDO
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
        /// 维修日期
        /// </summary>
        [Column("date")]
        public DateTime Date { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 维修费用
        /// </summary>
        [Column("cost")]
        public decimal Cost { get; set; }
        /// <summary>
        /// 供应商
        /// </summary>
        [Column("supplier")]
        public string Supplier { get; set; } = string.Empty;
        /// <summary>
        /// 维修内容
        /// </summary>
        [Column("content")]
        public string Content { get; set; } = string.Empty;
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