using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;

namespace Ae.Shop.Api.Dal.Model
{
    [Table("allot_product")]
    public class AllotProductDO
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
        [Column("task_id")]
        public long TaskId { get; set; }
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
        /// 状态(1新建 2已审核 3已取消 4已发出 5已驳回)
        /// </summary>
        [Column("status")]
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// 批次
        /// </summary>
        [Column("batch_id")]
        public long BatchId { get; set; }
        /// <summary>
        /// 外联批次
        /// </summary>
        [Column("ref_batch_id")]
        public long RefBatchId { get; set; }
        /// <summary>
        /// 货主
        /// </summary>
        [Column("owner_id")]
        public long OwnerId { get; set; }
        /// <summary>
        /// 库存单号
        /// </summary>
        [Column("stock_id")]
        public long StockId { get; set; }
        /// <summary>
        /// 成本
        /// </summary>
        [Column("cost_price")]
        public decimal CostPrice { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        [Column("unit")]
        public string Unit { get; set; } = string.Empty;
        /// <summary>
        /// 是否删除
        /// </summary>
        [Column("is_deleted")]
        public sbyte IsDeleted { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column("remark")]
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// 驳回人
        /// </summary>
        [Column("reject_by")]
        public string RejectBy { get; set; } = string.Empty;
        /// <summary>
        /// 驳回理由
        /// </summary>
        [Column("reject_reason")]
        public string RejectReason { get; set; } = string.Empty;
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