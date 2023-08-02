using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;

namespace Ae.Product.Service.Dal.Model
{
    [Table("flash_sale_config")]
    public class FlashSaleConfigDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 有效开始时间
        /// </summary>
        [Column("active_start_time")]
        public DateTime ActiveStartTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 有效结束时间
        /// </summary>
        [Column("active_end_time")]
        public DateTime ActiveEndTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 产品名称
        /// </summary>
        [Column("product_name")]
        public string ProductName { get; set; } = string.Empty;
        /// <summary>
        /// 产品编号
        /// </summary>
        [Column("product_id")]
        public string ProductId { get; set; } = string.Empty;
        /// <summary>
        /// 数量
        /// </summary>
        [Column("num")]
        public int Num { get; set; }

        /// <summary>
        /// 已售数量
        /// </summary>
        [Column("sale_num")]
        public int SaleNum { get; set; } = 0;
        /// <summary>
        /// 价格
        /// </summary>
        [Column("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// 状态(待审核,已生效,已结束,已取消)
        /// </summary>
        [Column("status")]
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// 删除标识
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
