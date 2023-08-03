using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;

namespace Ae.Shop.Api.Dal.Model
{
    [Table("inventory_batch_flow")]
    public class InventoryBatchFlowDO
    {
        /// <summary>
        /// 序号
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 门店id
        /// </summary>
        [Column("shop_id")]
        public long ShopId { get; set; }
        /// <summary>
        /// 门店名称
        /// </summary>
        [Column("shop_name")]
        public string ShopName { get; set; } = string.Empty;
        /// <summary>
        /// 来源单据Id
        /// </summary>
        [Column("source_inventory_id")]
        public long SourceInventoryId { get; set; }
        /// <summary>
        /// 来源总单号
        /// </summary>
        [Column("source_id")]
        public string SourceId { get; set; } = string.Empty;
        /// <summary>
        /// 来源单号 采购子单号/入库子单号
        /// </summary>
        [Column("source_inventory_no")]
        public string SourceInventoryNo { get; set; } = string.Empty;
        /// <summary>
        /// 操作类型(入库,出库)
        /// </summary>
        [Column("operation_type")]
        public string OperationType { get; set; } = string.Empty;
        /// <summary>
        /// 来源类型(采购入库 订单出库 盘盈入库 盘亏出库)
        /// </summary>
        [Column("source_type")]
        public string SourceType { get; set; } = string.Empty;
        /// <summary>
        /// 产品的pid
        /// </summary>
        [Column("product_id")]
        public string ProductId { get; set; } = string.Empty;
        /// <summary>
        /// 商品名称
        /// </summary>
        [Column("product_name")]
        public string ProductName { get; set; } = string.Empty;
        /// <summary>
        /// 库存id
        /// </summary>
        [Column("inventory_id")]
        public long InventoryId { get; set; }
        /// <summary>
        /// 批次编号
        /// </summary>
        [Column("batch_no")]
        public long BatchNo { get; set; }
        /// <summary>
        /// 外联批次号
        /// </summary>
        [Column("ref_batch_no")]
        public string RefBatchNo { get; set; } = string.Empty;
        /// <summary>
        /// 单价
        /// </summary>
        [Column("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        [Column("amount")]
        public decimal Amount { get; set; }
        /// <summary>
        /// 运费
        /// </summary>
        [Column("freight")]
        public decimal Freight { get; set; }
        /// <summary>
        /// 成本 非总成本
        /// </summary>
        [Column("cost")]
        public decimal Cost { get; set; }
        /// <summary>
        /// 修改前库存总量
        /// </summary>
        [Column("before_total_num")]
        public long BeforeTotalNum { get; set; }
        /// <summary>
        /// 修改后库存总量
        /// </summary>
        [Column("after_total_num")]
        public long AfterTotalNum { get; set; }
        /// <summary>
        /// 修改当前库存数量
        /// </summary>
        [Column("current_total_num")]
        public long CurrentTotalNum { get; set; }
        /// <summary>
        /// 修改前可用库存
        /// </summary>
        [Column("before_can_use_num")]
        public long BeforeCanUseNum { get; set; }
        /// <summary>
        /// 修改后可用库存
        /// </summary>
        [Column("after_can_use_num")]
        public long AfterCanUseNum { get; set; }
        /// <summary>
        /// 当前可用库存
        /// </summary>
        [Column("current_can_use_num")]
        public long CurrentCanUseNum { get; set; }
        /// <summary>
        /// 周期
        /// </summary>
        [Column("week_year")]
        public string WeekYear { get; set; } = string.Empty;
        /// <summary>
        /// 货主id
        /// </summary>
        [Column("own_id")]
        public long OwnId { get; set; }
        /// <summary>
        /// 货主类型 (仓库、门店、供应商(Company,Shop))
        /// </summary>
        [Column("own_type")]
        public string OwnType { get; set; } = string.Empty;
        /// <summary>
        /// 供应商Id
        /// </summary>
        [Column("supplier_id")]
        public string SupplierId { get; set; } = string.Empty;
        /// <summary>
        /// 供应商名称
        /// </summary>
        [Column("supplier_name")]
        public string SupplierName { get; set; } = string.Empty;
        /// <summary>
        /// 入库产品的属性（良品，不良品）
        /// </summary>
        [Column("product_attr_type")]
        public string ProductAttrType { get; set; } = string.Empty;
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
