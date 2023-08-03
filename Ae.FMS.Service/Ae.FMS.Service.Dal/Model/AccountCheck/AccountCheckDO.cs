using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;

namespace Ae.FMS.Service.Dal.Model
{
    [Table("account_check")]
    public class AccountCheckDO
    {
        /// <summary>
        /// 主键
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
        /// 订单渠道
        /// </summary>
        [Column("channel")]
        public string Channel { get; set; } = string.Empty;
        /// <summary>
        /// 订单用户手机号
        /// </summary>
        [Column("telephone")]
        public string Telephone { get; set; } = string.Empty;
        /// <summary>
        /// 门店编号
        /// </summary>
        [Column("location_id")]
        public long LocationId { get; set; }
        /// <summary>
        /// 门店名称
        /// </summary>
        [Column("location_name")]
        public string LocationName { get; set; } = string.Empty;
        /// <summary>
        /// 对账单类型(门店、平台，优惠成本承担方)
        /// </summary>
        [Column("account_type")]
        public string AccountType { get; set; } = string.Empty;
        /// <summary>
        /// 安装时间
        /// </summary>
        [Column("install_time")]
        public DateTime InstallTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 付款方式
        /// </summary>
        [Column("pay_method")]
        public string PayMethod { get; set; } = string.Empty;
        /// <summary>
        /// 到账状态
        /// </summary>
        [Column("money_arrive_status")]
        public string MoneyArriveStatus { get; set; } = string.Empty;
        /// <summary>
        /// 订单类型
        /// </summary>
        [Column("order_type")]
        public string OrderType { get; set; } = string.Empty;
        /// <summary>
        /// 订单总价（实付金额或套餐卡、核销码等的核销金额）
        /// </summary>
        [Column("sale_total_amount")]
        public decimal SaleTotalAmount { get; set; }
        /// <summary>
        /// 成本（作废）
        /// </summary>
        [Column("total_cost")]
        public decimal TotalCost { get; set; }
        /// <summary>
        /// 服务费（作废）
        /// </summary>
        [Column("service_fee")]
        public decimal ServiceFee { get; set; }
        /// <summary>
        /// 手续费(前期是固定的)（作废）
        /// </summary>
        [Column("commission_fee")]
        public decimal CommissionFee { get; set; }
        /// <summary>
        /// 其他费用（作废）
        /// </summary>
        [Column("other_fee")]
        public decimal OtherFee { get; set; }
        /// <summary>
        /// 结算价（作废）
        /// </summary>
        [Column("settlement_fee")]
        public decimal SettlementFee { get; set; }
        /// <summary>
        /// 门店对账状态（已核对 、核对异常）
        /// </summary>
        [Column("shop_check_result")]
        public string ShopCheckResult { get; set; } = string.Empty;
        /// <summary>
        /// 门店核对时间
        /// </summary>
        [Column("shop_check_time")]
        public DateTime ShopCheckTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 门店核对意见
        /// </summary>
        [Column("shop_check_suggestion")]
        public string ShopCheckSuggestion { get; set; } = string.Empty;
        /// <summary>
        /// 总部对账状态（已核对）
        /// </summary>
        [Column("rg_check_result")]
        public string RgCheckResult { get; set; } = string.Empty;
        /// <summary>
        /// 总部核对时间
        /// </summary>
        [Column("rg_check_time")]
        public DateTime RgCheckTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 总部核对意见
        /// </summary>
        [Column("rg_check_suggestion")]
        public string RgCheckSuggestion { get; set; } = string.Empty;
        /// <summary>
        /// 对账状态(备用)
        /// </summary>
        [Column("status")]
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// 提现状态(未申请、已申请、申请成功)
        /// </summary>
        [Column("withdraw_status")]
        public string WithdrawStatus { get; set; } = string.Empty;
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
        /// 差价（1. if 铺货毛利>=0 then 补差价=0，if 铺货毛利<0 then 补差价=-1*铺货毛利
        /// </summary>
        [Column("shop_difference_price")]
        public decimal ShopDifferencePrice { get; set; } 
        /// <summary>
        /// 手续费（1. （安装费+门店项目金额+铺货毛利+补差价-扣其他）* 0.005
        /// </summary>
        [Column("shop_commission_fee")]
        public decimal ShopCommissionFee { get; set; } 
        /// <summary>
        /// 门店结算金额(1. 安装费+门店项目金额+铺货毛利+补差价 - 扣其他 - 扣手续)
        /// </summary>
        [Column("shop_settlement_amount")]
        public decimal ShopSettlementAmount { get; set; } 
        /// <summary>
        /// 公司返现金额（1. 订单中所有铺货实物产品的sum(PID.返现价*数量）
        /// </summary>
        [Column("company_back_amount")]
        public decimal CompanyBackAmount { get; set; }
        /// <summary>
        /// 门店其他费用
        /// </summary>
        [Column("shop_other_fee")]
        public decimal ShopOtherFee { get; set; }
        /// <summary>
        /// 门店外采项目费
        /// </summary>
        [Column("shop_item_fee")]
        public decimal ShopItemFee { get; set; }
        /// <summary>
        /// 门店外采成本
        /// </summary>
        [Column("shopOutProductCost")]
        public decimal ShopOutProductCost { get; set; }
        /// <summary>
        /// 公司扣手续费
        ///(1. （公司返现金额 - 公司扣其他）* 0.005)
        /// </summary>
        [Column("company_commission_fee")]
        public decimal CompanyCommissionFee { get; set; }
        /// <summary>
        /// 公司应结算金额
        ///(1. 公司返现金额 - 公司扣其他 - 公司扣手续费
        /// </summary>
        [Column("company_settlement_amount")]
        public decimal CompanySettlementAmount { get; set; }
        /// <summary>
        /// 公司其他费用
        /// </summary>
        [Column("company_other_fee")]
        public decimal CompanyOtherFee { get; set; }
        /// <summary>
        /// 产品明细
        /// </summary>
        [Column("product_detail")]
        public string ProductDetail { get; set; } = string.Empty;
    }
}