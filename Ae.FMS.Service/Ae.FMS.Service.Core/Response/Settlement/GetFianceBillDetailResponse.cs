using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.FMS.Service.Core.Response.Settlement
{
    /// <summary>
    /// 财务账单详情返回对象
    /// </summary>
    public class GetFianceBillDetailResponse
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 展示的安装时间
        /// </summary>
        public DateTime InstallTime { get; set; }

        /// <summary>
        /// 订单类型
        /// </summary>
        public string OrderType { get; set; }


        /// <summary>
        /// 显示支付方式
        /// </summary>
        public string PayMethod { get; set; }

        /// <summary>
        /// 销售总价
        /// </summary>
        public decimal SaleTotalAmount { get; set; }

        /// <summary>
        /// 成本
        /// </summary>
        public decimal TotalCost { get; set; }

        /// <summary>
        /// 服务的价格
        /// </summary>
        public decimal ServiceFee { get; set; }

        /// <summary>
        /// 扣减的手续费
        /// </summary>
        public decimal CommissionFee { get; set; }

        /// <summary>
        /// 扣减其他
        /// </summary>
        public decimal OtherFee { get; set; }

        /// <summary>
        /// 应结算的价格
        /// </summary>
        public decimal SettlementFee { get; set; }

        /// <summary>
        /// 门店核对时间
        /// </summary>
        public DateTime ShopCheckTime { get; set; }

        /// <summary>
        /// 门店核对结果
        /// </summary>
        public string ShopCheckResult { get; set; }


        /// <summary>
        /// 门店核对意见
        /// </summary>
        public string ShopCheckSuggestion { get; set; }

        /// <summary>
        /// 总部核对意见
        /// </summary>
        public string RGCheckSuggestion { get; set; }

        /// <summary>
        /// 公司id
        /// </summary>
        public long CompanyId { get; set; }
        /// <summary>
        /// 公司全称
        /// </summary>
        public string CompanyName { get; set; } = string.Empty;
        /// <summary>
        /// 服务安装费
        /// </summary>
        public decimal ShopInstallAmount { get; set; }
        /// <summary>
        /// 实付金额
        /// </summary>
        public decimal ActualAmount { get; set; }
        /// <summary>
        /// 铺货成本（1. sum(PID.门店结算价*数量)

        /// </summary>
        public decimal ShopDistributionCost { get; set; }
        /// <summary>
        /// 铺货毛利（1. 订单实付金额 - 安装费 - 铺货成本 - 门店项目金额）
        /// </summary>
        public decimal ShopDistributionGrossProfit { get; set; }
        /// <summary>
        /// 差价（1. if 铺货毛利>=0 then 补差价=0，if 铺货毛利<0 then 补差价=-1*铺货毛利
        /// /// </summary>
        public decimal ShopDifferencePrice { get; set; }
        /// <summary>
        /// 手续费（1. （安装费+门店项目金额+铺货毛利+补差价-扣其他）* 0.005
        /// </summary>
        public decimal ShopCommissionFee { get; set; }
        /// <summary>
        /// 门店结算金额(1. 安装费+门店项目金额+铺货毛利+补差价 - 扣其他 - 扣手续费
        /// </summary>
        public decimal ShopSettlementAmount { get; set; }
        /// <summary>
        /// 公司返现金额（1. 订单中所有铺货实物产品的sum(PID.返现价*数量)
        /// </summary>
        public decimal CompanyBackAmount { get; set; }
        /// <summary>
        /// 门店其他费用
        /// </summary>
        public decimal ShopOtherFee { get; set; }
        /// <summary>
        /// 门店项目费
        /// </summary>
        public decimal ShopItemFee { get; set; }
        /// <summary>
        /// 公司扣手续费(1. （公司返现金额 - 公司扣其他）* 0.005
        /// </summary>
        public decimal CompanyCommissionFee { get; set; }
        /// <summary>
        /// 公司应结算金额
        ///(1. 公司返现金额 - 公司扣其他 - 公司扣手续费
        /// </summary>
        public decimal CompanySettlementAmount { get; set; }
        /// <summary>
        /// 公司其他费用
        /// </summary>
        public decimal CompanyOtherFee { get; set; }


    }
}
