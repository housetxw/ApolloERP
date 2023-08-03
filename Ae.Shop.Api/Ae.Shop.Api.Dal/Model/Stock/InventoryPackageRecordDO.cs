using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;

namespace Ae.Shop.Api.Dal.Model
{
    [Table("inventory_package_record")]
    public class InventoryPackageRecordDO
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
        /// 铺货类型
        /// </summary>
        [Column("transfer_type")]
        public string TransferType { get; set; } = string.Empty;
        /// <summary>
        /// 铺货产品单号
        /// </summary>
        [Column("transfer_product_id")]
        public long TransferProductId { get; set; }
        /// <summary>
        /// 包裹号
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
        /// 实收数量
        /// </summary>
        [Column("num")]
        public int Num { get; set; }
        /// <summary>
        /// 破损数量
        /// </summary>
        [Column("bad_num")]
        public int BadNum { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [Column("status")]
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// 备注
        /// </summary>
        [Column("remark")]
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// 是否删除
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