using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;

namespace Ae.Shop.Api.Dal.Model
{
    [Table("inventory_exception_record")]
    public class InventoryExceptionRecordDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 铺货单号
        /// </summary>
        [Column("transfer_id")]
        public string TransferId { get; set; } = string.Empty;
        /// <summary>
        /// 铺货类型(铺货,调拨)
        /// </summary>
        [Column("transfer_type")]
        public string TransferType { get; set; } = string.Empty;
        /// <summary>
        /// 铺货产品单号
        /// </summary>
        [Column("transfer_product_id")]
        public long TransferProductId { get; set; }
        /// <summary>
        /// 包裹单号
        /// </summary>
        [Column("package_no")]
        public string PackageNo { get; set; } = string.Empty;
        /// <summary>
        /// 产品编号
        /// </summary>
        [Column("product_id")]
        public string ProductId { get; set; } = string.Empty;
        /// <summary>
        /// 产品名称
        /// </summary>
        [Column("product_name")]
        public string ProductName { get; set; } = string.Empty;
        /// <summary>
        /// 货损数量
        /// </summary>
        [Column("num")]
        public int Num { get; set; }
        /// <summary>
        /// 货损类型
        /// </summary>
        [Column("exception_type")]
        public string ExceptionType { get; set; } = string.Empty;
        /// <summary>
        /// 备注
        /// </summary>
        [Column("remark")]
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// 状态(新建,已处理)
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