using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Dal.Model
{
    public class ShopServiceInfoVO
    {
        /// <summary>
        /// 主键id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 门店id
        /// </summary>
        public long ShopId { get; set; }
        /// <summary>
        /// 基础服务ID
        /// </summary>
        public long BaseServiceId { get; set; }
        /// <summary>
        /// 产品id
        /// </summary>
        public string ProductId { get; set; } = string.Empty;
        /// <summary>
        /// 产品名称
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 成本价
        /// </summary>
        public decimal CostPrice { get; set; }
        /// <summary>
        /// 销售价
        /// </summary>
        public decimal SalePrice { get; set; }
        /// <summary>
        /// 默认销售价
        /// </summary>
        public decimal DefaultSalePrice { get; set; }
        /// <summary>
        /// 默认成本价
        /// </summary>
        public decimal DefaultCostPrice { get; set; }
        /// <summary>
        /// 默认销售价格限制
        /// </summary>
        public decimal DefaultSalePriceLimit { get; set; }
        /// <summary>
        /// 成本价限制
        /// </summary>
        public decimal DefaultCostPriceLimit { get; set; }
        /// <summary>
        /// 服务备注
        /// </summary>
        public string ServiceRemark { get; set; } = string.Empty;
        /// <summary>
        /// 价格备注
        /// </summary>
        public string SalePriceRemark { get; set; } = string.Empty;
        /// <summary>
        /// 服务费
        /// </summary>
        public decimal ServiceCharge { get; set; }
        /// <summary>
        /// 上下架状态 0下架  1上架
        /// </summary>
        public sbyte Status { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; } = string.Empty;
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);
    }
}
