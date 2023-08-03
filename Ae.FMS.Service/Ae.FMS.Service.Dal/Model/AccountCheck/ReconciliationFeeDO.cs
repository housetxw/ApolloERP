using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;

namespace Ae.FMS.Service.Dal.Model.AccountCheck
{
    [Table("reconciliation_fee")]
    public class ReconciliationFeeDO
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        [Column("order_no")]
        public string OrderNo { get; set; } = string.Empty;
        /// <summary>
        /// 订单类型
        /// </summary>
        [Column("order_type")]
        public sbyte OrderType { get; set; }
        /// <summary>
        /// 产生类型
        /// </summary>
        [Column("produce_type")]
        public sbyte ProduceType { get; set; }
        /// <summary>
        /// 对账单类型(门店、平台，优惠成本承担方)
        /// </summary>
        [Column("account_type")]
        public string AccountType { get; set; } = string.Empty;
        /// <summary>
        /// 门店Id
        /// </summary>
        [Column("shop_id")]
        public long ShopId { get; set; }
        /// <summary>
        /// 门店名称
        /// </summary>
        [Column("shop_name")]
        public string ShopName { get; set; } = string.Empty;
        /// <summary>
        /// 公司id
        /// </summary>
        [Column("company_id")]
        public long CompanyId { get; set; }
        /// <summary>
        /// 公司全称
        /// </summary>
        [Column("company_name")]
        public string CompanyName { get; set; } = string.Empty;
        /// <summary>
        /// 订单总价（实付金额或套餐卡、核销码等的核销金额）
        /// </summary>
        [Column("sale_total_amount")]
        public decimal SaleTotalAmount { get; set; }
        /// <summary>
        /// 服务安装费
        /// </summary>
        [Column("shop_install_amount")]
        public decimal ShopInstallAmount { get; set; }
        /// <summary>
        /// 实付金额
        /// </summary>
        [Column("actual_amount")]
        public decimal ActualAmount { get; set; }
        /// <summary>
        /// 铺货成本（1. sum(PID.门店结算价*数量)
        /// </summary>
        [Column("shop_distribution_cost")]
        public decimal ShopDistributionCost { get; set; }
        /// <summary>
        /// 铺货毛利（1. 订单实付金额 - 安装费 - 铺货成本 - 门店项目金额）
        /// </summary>
        [Column("shop_distribution_gross_profit")]
        public decimal ShopDistributionGrossProfit { get; set; }

        /// <summary>
        /// 补差价（1. if 铺货毛利>=0 then 补差价=0，
        /// if 铺货毛利&lt;0 then 补差价=(-1*铺货毛利）)
        /// </summary>
        [Column("shop_difference_price")]
        public decimal ShopDifferencePrice { get; set; } 
        /// <summary>
        /// 手续费（1. （安装费+门店项目金额+铺货毛利+补差价-扣其他）* 0.005）
        /// </summary>
        [Column("shop_commission_fee")]
        public decimal ShopCommissionFee { get; set; } 
        /// <summary>
        /// 门店结算金额(1. 安装费+门店项目金额+铺货毛利+补差价 - 扣其他 - 扣手续费)
        /// </summary>
        [Column("shop_settlement_amount")]
        public decimal ShopSettlementAmount { get; set; }
        /// <summary>
        /// 门店扣其他费用
        /// </summary>
        [Column("shop_other_fee")]
        public decimal ShopOtherFee { get; set; }
        /// <summary>
        /// 门店外采金额
        /// </summary>
        [Column("shop_Item_fee")]
        public decimal ShopItemFee { get; set; }

        /// <summary>
        /// 外采成本
        /// </summary>
        [Column("shop_item_cost")]
        public decimal ShopItemCost { get; set; }
        /// <summary>
        /// 公司返现金额（1. 订单中所有铺货实物产品的sum(PID.返现价*数量)）
        /// </summary>
        [Column("company_back_amount")]
        public decimal CompanyBackAmount { get; set; }
        /// <summary>
        /// 公司扣手续费
        ///(1. （公司返现金额 - 公司扣其他）* 0.005)
        /// </summary>
        [Column("company_commission_fee")]
        public decimal CompanyCommissionFee { get; set; }
        /// <summary>
        /// 公司应结算金额
        ///(1. 公司返现金额 - 公司扣其他 - 公司扣手续费)
        /// </summary>
        [Column("company_settlement_amount")]
        public decimal CompanySettlementAmount { get; set; }

        /// <summary>
        /// 公司其他费用
        /// </summary>
        [Column("company_other_fee")]
        public decimal CompanyOtherFee { get; set; }
        /// <summary>
        /// 删除标识 0未删除 1已删除
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
        /// <summary>
        /// 产品明细
        /// </summary>
        [Column("product_detail")]
        public string ProductDetail { get; set; } = string.Empty;
    }
}
