using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Model.ShopPurchase
{
    public class PurchaseOrderProductDTO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 总采购单号
        /// </summary>
        public long PoId { get; set; }
        /// <summary>
        /// 产品编号
        /// </summary>
        public string ProductCode { get; set; } = string.Empty;
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; } = string.Empty;
        /// <summary>
        /// 采购成本价格
        /// </summary>
        public decimal PurchaseCost { get; set; }
        /// <summary>
        /// 总成本
        /// </summary>
        public decimal TotalCost { get; set; }
        /// <summary>
        /// 采购数量 
        /// </summary>
        public int Num { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// 税点
        /// </summary>
        public decimal TaxPoint { get; set; }
        /// <summary>
        /// 销售单价
        /// </summary>
        public decimal SalePrice { get; set; }
        /// <summary>
        /// 采购价格不含税
        /// </summary>
        public decimal PurchasePrice { get; set; }
        /// <summary>
        /// 采购总价不含税
        /// </summary>
        public decimal TotalPrice { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// 计划入库时间
        /// </summary>
        public DateTime PlanInstockDate { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 已入库数量
        /// </summary>
        public int InstockNum { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; } = string.Empty;
        /// <summary>
        /// 状态(1新建 2待审核 3已入库 4已取消)
        /// </summary>
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// 删除标识 0否 1是
        /// </summary>
        public int IsDeleted { get; set; }
        /// <summary>
        /// 修改前采购价格
        /// </summary>
        public decimal OldPurchaseCost { get; set; }
        /// <summary>
        /// 退货数量 
        /// </summary>
        public int BackNum { get; set; }
        public int EditBackNum { get; set; }

    }
}
