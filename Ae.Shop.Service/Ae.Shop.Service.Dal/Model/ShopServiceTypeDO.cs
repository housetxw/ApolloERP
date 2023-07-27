using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;
using Ae.Shop.Service.Common.Helper;

namespace Ae.Shop.Service.Dal.Model
{
    [Table("shop_service_type")]
    public class ShopServiceTypeDO
    {
        /// <summary>
        /// 主键id
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; } 
        /// <summary>
        /// 门店id
        /// </summary>
        [Column("shop_id")]
        [CompareDiffAttribute(Name = "门店id")]
        public long ShopId { get; set; } 
        /// <summary>
        /// 服务类型
        /// </summary>
        [Column("service_type")]
        [CompareDiffAttribute(Name = "服务类型")]
        public string ServiceType { get; set; } = string.Empty;
        /// <summary>
        /// 是否删除 0否 1是
        /// </summary>
        [Column("is_deleted")]
        [CompareDiffAttribute(Name = "删除标识")]
        public sbyte IsDeleted { get; set; } 
        /// <summary>
        /// 创建人
        /// </summary>
        [Column("create_by")]
        [CompareDiffAttribute(Name = "创建人")]
        public string CreateBy { get; set; } = string.Empty;
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("create_time")]
        [CompareDiffAttribute(Name = "创建时间")]
        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 修改人
        /// </summary>
        [Column("update_by")]
        [CompareDiffAttribute(Name = "修改人")]
        public string UpdateBy { get; set; } = string.Empty;
        /// <summary>
        /// 修改时间
        /// </summary>
        [Column("update_time")]
        [CompareDiffAttribute(Name = "修改时间")]
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);
    }
}