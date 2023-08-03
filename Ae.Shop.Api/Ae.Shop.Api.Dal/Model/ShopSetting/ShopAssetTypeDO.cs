using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;

namespace Ae.Shop.Api.Dal.Model
{
    [Table("shop_asset_type")]
    public class ShopAssetTypeDO
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        /// <summary>
        /// 父级ID
        /// </summary>
        [Column("parent_id")]
        public string ParentId { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        [Column("value")]
        public string Value { get; set; } = string.Empty;
        /// <summary>
        /// 名
        /// </summary>
        [Column("label")]
        public string Label { get; set; } = string.Empty;
        /// <summary>
        /// 删除标识
        /// </summary>
        [Column("is_deleted")]
        public sbyte IsDeleted { get; set; }
    }
}