using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;

namespace Ae.Shop.Api.Dal.Model.WMS
{
    [Table("batch")]
    public class BatchDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 采购子单号
        /// </summary>
        [Column("po_id")]
        public long PoId { get; set; }
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
        /// 成本
        /// </summary>
        [Column("cost")]
        public decimal Cost { get; set; }
        /// <summary>
        /// 入库时间
        /// </summary>
        [Column("in_stock_time")]
        public DateTime InStockTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 周期
        /// </summary>
        [Column("week_year")]
        public string WeekYear { get; set; } = string.Empty;
        /// <summary>
        /// 入库操作人
        /// </summary>
        [Column("in_stock_name")]
        public string InStockName { get; set; } = string.Empty;
        /// <summary>
        /// 供应商编号
        /// </summary>
        [Column("vender_id")]
        public long VenderId { get; set; }
        /// <summary>
        /// 供应商简称
        /// </summary>
        [Column("vender_short_name")]
        public string VenderShortName { get; set; } = string.Empty;
        /// <summary>
        /// 商品批次
        /// </summary>
        [Column("product_batch_id")]
        public int ProductBatchId { get; set; }
        /// <summary>
        /// 产品生产时间
        /// </summary>
        [Column("production_date")]
        public DateTime ProductionDate { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 渠道
        /// </summary>
        [Column("channel")]
        public string Channel { get; set; } = string.Empty;
        /// <summary>
        /// 库存单位
        /// </summary>
        [Column("stock_unit")]
        public string StockUnit { get; set; } = string.Empty;
        /// <summary>
        /// 货主
        /// </summary>
        [Column("owner_id")]
        public int OwnerId { get; set; }
        /// <summary>
        /// 货主类型
        /// </summary>
        [Column("owner_type")]
        public string OwnerType { get; set; } = string.Empty;
        /// <summary>
        /// 货主名称
        /// </summary>
        [Column("owner_name")]
        public string OwnerName { get; set; } = string.Empty;
        /// <summary>
        /// 税率
        /// </summary>
        [Column("tax_rate")]
        public int TaxRate { get; set; }
        /// <summary>
        /// 无税成本
        /// </summary>
        [Column("no_tax_cost")]
        public decimal NoTaxCost { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column("remark")]
        public string Remark { get; set; } = string.Empty;
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
