using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Dal.Model.ShopPurchase
{

    [Table("purchase_order_product")]
    public class PurchaseOrderProductDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 总采购单号
        /// </summary>
        [Column("po_id")]
        public long PoId { get; set; }
        /// <summary>
        /// 产品编号
        /// </summary>
        [Column("product_code")]
        public string ProductCode { get; set; } = string.Empty;
        /// <summary>
        /// 产品名称
        /// </summary>
        [Column("product_name")]
        public string ProductName { get; set; } = string.Empty;
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
        /// 采购数量 
        /// </summary>
        [Column("num")]
        public int Num { get; set; }
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
        /// 税点
        /// </summary>
        [Column("tax_point")]
        public decimal TaxPoint { get; set; }
        /// <summary>
        /// 销售单价
        /// </summary>
        [Column("sale_price")]
        public decimal SalePrice { get; set; }
        /// <summary>
        /// 采购价格不含税
        /// </summary>
        [Column("purchase_price")]
        public decimal PurchasePrice { get; set; }
        /// <summary>
        /// 采购总价不含税
        /// </summary>
        [Column("total_price")]
        public decimal TotalPrice { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column("remark")]
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// 计划入库时间
        /// </summary>
        [Column("plan_instock_date")]
        public DateTime PlanInstockDate { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 已入库数量
        /// </summary>
        [Column("instock_num")]
        public int InstockNum { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        [Column("unit")]
        public string Unit { get; set; } = string.Empty;
        /// <summary>
        /// 状态(1新建 2预约中 3已取消 4待审核 5待财务审核 6已驳回 7审核通过 8已发货 9部分收货 10已收货)
        /// </summary>
        [Column("status")]
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// 删除标识 0否 1是
        /// </summary>
        [Column("is_deleted")]
        public int IsDeleted { get; set; }
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
        /// 分类名称
        /// </summary>
        [Column("category_name")]
        public string CategoryName { get; set; } = string.Empty;

        [Column("stock_id")]
        public string StockId { get; set; }
        /// <summary>
        /// 退货数量
        /// </summary>
        [Column("back_num")]
        public int BackNum { get; set; }


    }
}




































