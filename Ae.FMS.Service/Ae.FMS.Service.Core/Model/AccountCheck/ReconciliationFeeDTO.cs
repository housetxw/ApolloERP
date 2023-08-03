using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.FMS.Service.Core.Model.AccountCheck
{
    public class ReconciliationFeeDTO
    {
        /// <summary>
        /// 
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; } = string.Empty;
        /// <summary>
        /// 订单类型
        /// </summary>
        public sbyte OrderType { get; set; }
        /// <summary>
        /// 门店Id
        /// </summary>
        public long ShopId { get; set; }
        /// <summary>
        /// 门店名称
        /// </summary>
        public string ShopName { get; set; } = string.Empty;
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
        /// <summary>
        /// 删除标识 0未删除 1已删除
        /// </summary>
        public sbyte IsDeleted { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 修改人
        /// </summary>
        public string UpdateBy { get; set; } = string.Empty;
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);
    }
}
