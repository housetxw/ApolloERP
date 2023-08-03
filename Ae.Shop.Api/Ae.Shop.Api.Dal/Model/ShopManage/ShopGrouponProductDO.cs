using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Dal.Model.ShopManage
{
    [Table("shop_groupon_product")]
    public class ShopGrouponProductDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }

        /// <summary>
        /// 门店Id
        /// </summary>
        [Column("shop_id")]
        public long ShopId { get; set; }

        /// <summary>
        /// 服务pid
        /// </summary>
        [Column("service_id")]
        public string ServiceId { get; set; } = string.Empty;

        /// <summary>
        /// 服务名称
        /// </summary>
        [Column("service_name")]
        public string ServiceName { get; set; } = string.Empty;

        /// <summary>
        /// 团购售价
        /// </summary>
        [Column("price")]
        public decimal Price { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Column("status")]
        public sbyte Status { get; set; }

        /// <summary>
        /// 删除标记
        /// </summary>
        [Column("is_deleted")]
        public sbyte IsDeleted { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [Column("create_by")]
        public string CreateBy { get; set; } = string.Empty;

        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("create_time")]
        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);

        /// <summary>
        /// 更新人
        /// </summary>
        [Column("update_by")]
        public string UpdateBy { get; set; } = string.Empty;

        /// <summary>
        /// 更新时间
        /// </summary>
        [Column("update_time")]
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);
    }
}
