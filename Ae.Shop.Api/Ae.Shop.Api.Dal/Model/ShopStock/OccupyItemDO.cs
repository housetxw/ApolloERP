using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;

namespace Ae.Shop.Api.Dal.Model
{
    [Table("occupy_item")]
    public class OccupyItemDO
    {
        /// <summary>
        /// 序号
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 占用queueId
        /// </summary>
        [Column("occupy_queue_id")]
        public long OccupyQueueId { get; set; }
        /// <summary>
        /// 来源单据Id
        /// </summary>
        [Column("source_inventory_id")]
        public long SourceInventoryId { get; set; }
        /// <summary>
        /// 关联单号
        /// </summary>
        [Column("source_inventory_no")]
        public string SourceInventoryNo { get; set; } = string.Empty;
        /// <summary>
        /// 订单产品单号
        /// </summary>
        [Column("order_product_id")]
        public long OrderProductId { get; set; }
        /// <summary>
        /// 产品pid
        /// </summary>
        [Column("product_id")]
        public string ProductId { get; set; } = string.Empty;
        /// <summary>
        /// 产品名称
        /// </summary>
        [Column("product_name")]
        public string ProductName { get; set; } = string.Empty;
        /// <summary>
        /// 库存id
        /// </summary>
        [Column("inventory_id")]
        public long InventoryId { get; set; }
        /// <summary>
        /// 仓库id
        /// </summary>
        [Column("location_id")]
        public long LocationId { get; set; }
        /// <summary>
        /// 占库数量
        /// </summary>
        [Column("num")]
        public long Num { get; set; }
        /// <summary>
        /// 批次号
        /// </summary>
        [Column("batch_no")]
        public string BatchNo { get; set; } = string.Empty;
        /// <summary>
        /// 外联批次单号
        /// </summary>
        [Column("ref_batch_no")]
        public string RefBatchNo { get; set; } = string.Empty;
        /// <summary>
        /// 成本
        /// </summary>
        [Column("cost")]
        public decimal Cost { get; set; }
        /// <summary>
        /// 占用类型(订单，退货的应该分开)
        /// </summary>
        [Column("occupy_type")]
        public string OccupyType { get; set; } = string.Empty;
        /// <summary>
        /// 货主
        /// </summary>
        [Column("own_id")]
        public long OwnId { get; set; }
        /// <summary>
        /// 货主类型 (仓库、门店、供应商(Company,Shop))
        /// </summary>
        [Column("own_type")]
        public string OwnType { get; set; } = string.Empty;
        /// <summary>
        /// 库存类型(1良品 2不良品)
        /// </summary>
        [Column("stock_type")]
        public sbyte StockType { get; set; }
        /// <summary>
        /// 是否有效(1有效-还在占用中 2无效-订单已安装  3.占用释放)
        /// </summary>
        [Column("is_valid")]
        public sbyte IsValid { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Column("remark")]
        public string Remark { get; set; }
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
    }
}