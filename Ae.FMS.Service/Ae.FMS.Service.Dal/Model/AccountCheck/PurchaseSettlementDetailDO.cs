using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ae.FMS.Service.Dal.Model.AccountCheck
{
    [Table("purchase_settlement_detail")]
    public class PurchaseSettlementDetailDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 结算批次Id
        /// </summary>
        [Column("settlement_batch_id")]
        public long SettlementBatchId { get; set; }
        /// <summary>
        /// 结算批次no
        /// </summary>
        [Column("settlement_batch_no")]
        public string SettlementBatchNo { get; set; } = string.Empty;
        /// <summary>
        /// 门店Id
        /// </summary>
        [Column("location_id")]
        public long LocationId { get; set; }
        /// <summary>
        /// 门店名称
        /// </summary>
        [Column("location_name")]
        public string LocationName { get; set; } = string.Empty;
        /// <summary>
        /// 结算人员
        /// </summary>
        [Column("settlement_staff")]
        public string SettlementStaff { get; set; } = string.Empty;
        /// <summary>
        /// 结算方式( 现金  、月结)
        /// </summary>
        [Column("settlement_type")]
        public string SettlementType { get; set; } = string.Empty;
        /// <summary>
        /// 采购单号
        /// </summary>
        [Column("purchase_no")]
        public string PurchaseNo { get; set; } = string.Empty;

        /// <summary>
        /// 采购产品
        /// </summary>
        [Column("purchase_product_id")]
        public long purchaseProductId { get; set; }
        /// <summary>
        /// 采购日期
        /// </summary>
        [Column("purchase_date")]
        public DateTime PurchaseDate { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 产品类目
        /// </summary>
        [Column("product_category")]
        public string ProductCategory { get; set; } = string.Empty;
        /// <summary>
        /// 产品编码
        /// </summary>
        [Column("product_code")]
        public string ProductCode { get; set; } = string.Empty;
        /// <summary>
        /// 产品名称
        /// </summary>
        [Column("product_name")]
        public string ProductName { get; set; } = string.Empty;
        /// <summary>
        /// 型号规格
        /// </summary>
        [Column("specification")]
        public string Specification { get; set; } = string.Empty;
        /// <summary>
        /// 原厂编号
        /// </summary>
        [Column("oe_number")]
        public string OeNumber { get; set; } = string.Empty;
        /// <summary>
        /// 单位
        /// </summary>
        [Column("unit")]
        public string Unit { get; set; } = string.Empty;
        /// <summary>
        /// 采购数量 
        /// </summary>
        [Column("num")]
        public int Num { get; set; }
        /// <summary>
        /// 采购成本价格
        /// </summary>
        [Column("purchase_cost")]
        public decimal PurchaseCost { get; set; }
        /// <summary>
        /// 总成本
        /// </summary>
        [Column("total_cost")]
        public decimal TotalCost { get; set; }
        /// <summary>
        /// 结算的金额
        /// </summary>
        [Column("settlement_amount")]
        public decimal SettlementAmount { get; set; }
        /// <summary>
        /// 门店结算的费用
        /// </summary>
        [Column("shop_commission_fee")]
        public decimal ShopCommissionFee { get; set; }
        /// <summary>
        /// 其他费用
        /// </summary>
        [Column("shop_other_fee")]
        public decimal ShopOtherFee { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column("remark")]
        public string Remark { get; set; } = string.Empty;
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
