using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.FMS.Service.Core.Model.AccountCheck
{
    public class PurchaseAccountCheckDTO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 门店Id
        /// </summary>
        public long LoactionId { get; set; }
        /// <summary>
        /// 结算人员
        /// </summary>
        public string SettlementStaff { get; set; } = string.Empty;
        /// <summary>
        /// 结算方式( 现金  、月结)
        /// </summary>
        public string SettlementType { get; set; } = string.Empty;
        /// <summary>
        /// 采购单号
        /// </summary>
        public string PurchaseNo { get; set; } = string.Empty;
        /// <summary>
        /// 采购日期
        /// </summary>
        public DateTime PurchaseDate { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 产品类目
        /// </summary>
        public string ProductCategory { get; set; } = string.Empty;
        /// <summary>
        /// 产品编码
        /// </summary>
        public string ProductCode { get; set; } = string.Empty;
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; } = string.Empty;
        /// <summary>
        /// 型号规格
        /// </summary>
        public string Specification { get; set; } = string.Empty;
        /// <summary>
        /// 原厂编号
        /// </summary>
        public string OeNumber { get; set; } = string.Empty;
        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; } = string.Empty;
        /// <summary>
        /// 采购数量 
        /// </summary>
        public int Num { get; set; }
        /// <summary>
        /// 采购成本价格
        /// </summary>
        public decimal PurchaseCost { get; set; }
        /// <summary>
        /// 总成本
        /// </summary>
        public decimal TotalCost { get; set; }
        /// <summary>
        /// 结算的金额
        /// </summary>
        public decimal SettlementAmount { get; set; }
        /// <summary>
        /// 门店结算的费用
        /// </summary>
        public decimal ShopCommissionFee { get; set; }
        /// <summary>
        /// 其他费用
        /// </summary>
        public decimal ShopOtherFee { get; set; }
        /// <summary>
        /// 状态（已核对 、核对异常、未核对）
        /// </summary>
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// 删除标识
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
