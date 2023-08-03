using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;

namespace Ae.Shop.Api.Dal.Model
{
    [Table("stock_inout_item")]
    public class StockInoutItemDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 出入库编号
        /// </summary>
        [Column("inout_id")]
        public long InoutId { get; set; }
        /// <summary>
        /// 关联子单号
        /// </summary>
        [Column("releation_id")]
        public long ReleationId { get; set; }
        /// <summary>
        /// 商品名称Id
        /// </summary>
        [Column("product_id")]
        public string ProductId { get; set; } = string.Empty;
        /// <summary>
        /// 产品名称
        /// </summary>
        [Column("product_name")]
        public string ProductName { get; set; } = string.Empty;
        /// <summary>
        /// 批次编号
        /// </summary>
        [Column("batch_no")]
        public string BatchNo { get; set; } = string.Empty;
        /// <summary>
        /// 供应商编号
        /// </summary>
        [Column("supplier_id")]
        public long SupplierId { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        [Column("supplier_name")]
        public string SupplierName { get; set; } = string.Empty;
        /// <summary>
        /// 出入库总数
        /// </summary>
        [Column("num")]
        public int Num { get; set; }
        /// <summary>
        /// 已入数量
        /// </summary>
        [Column("actual_num")]
        public int ActualNum { get; set; }
        /// <summary>
        /// 良品数
        /// </summary>
        [Column("good_num")]
        public int GoodNum { get; set; }
        /// <summary>
        /// 不良品数
        /// </summary>
        [Column("bad_num")]
        public int BadNum { get; set; }
        /// <summary>
        /// 成本单价
        /// </summary>
        [Column("cost")]
        public decimal Cost { get; set; }
        /// <summary>
        /// 单位文本
        /// </summary>
        [Column("uom_text")]
        public string UomText { get; set; } = string.Empty;
        /// <summary>
        /// 状态
        /// </summary>
        [Column("status")]
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// 删除标识
        /// </summary>
        [Column("is_deleted")]
        public sbyte IsDeleted { get; set; }
        /// <summary>
        /// 创建用户ID
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
        public int AvailableNum { get; set; }

        /// <summary>
        /// 1 按主键更新 2按关联单号更新
        /// </summary>
        [NotMapped]
        public int UpdateType { get; set; }
    }
}