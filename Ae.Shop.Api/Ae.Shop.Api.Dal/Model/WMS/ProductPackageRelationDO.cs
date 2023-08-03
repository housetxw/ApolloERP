using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;
namespace Ae.Shop.Api.Dal.Model
{
    [Table("product_package_relation")]
    public class ProductPackageRelationDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 出库单号
        /// </summary>
        [Column("transfer_id")]
        public long TransferId { get; set; }
        /// <summary>
        /// 出库产品单号
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
        /// 数量
        /// </summary>
        [Column("num")]
        public int Num { get; set; }
        /// <summary>
        /// 已收数量
        /// </summary>
        [Column("receive_num")]
        public int ReceiveNum { get; set; }
        /// <summary>
        /// 批次号
        /// </summary>
        [Column("batch_id")]
        public long BatchId { get; set; }
        /// <summary>
        /// 状态(新建 已发出 已签收 已清点)
        /// </summary>
        [Column("status")]
        public string Status { get; set; } = string.Empty;
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

        [NotMapped]
        public List<string> PackageNos { get; set; } = new List<string>();
    }
}
