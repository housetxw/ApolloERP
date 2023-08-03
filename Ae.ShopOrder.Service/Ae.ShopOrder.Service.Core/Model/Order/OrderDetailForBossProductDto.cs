using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Model.Order
{
    public class OrderDetailForBossProductDto
    {
        /// <summary>
        /// 订单产品Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 产品Id
        /// </summary>
        public string ProductId { get; set; } = string.Empty;
        /// <summary>
        ///  产品性质，分为 0 实物产品、1 套餐产品、2 服务产品、3 数字产品 4 项目产品 5门店外采
        /// </summary>
        public sbyte ProductAttribute { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; } = string.Empty;
        /// <summary>
        /// 图片路径
        /// </summary>
        public string ImageUrl { get; set; } = string.Empty;
        /// <summary>
        /// 产品显示名称
        /// </summary>
        public string DisplayName { get; set; } = string.Empty;
        /// <summary>
        /// 产品描述
        /// </summary>
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// 标签
        /// </summary>
        public string Label { get; set; } = string.Empty;
        /// <summary>
        /// 单个数量
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// 单位（个/升/斤等）
        /// </summary>
        public string Unit { get; set; } = string.Empty;
        /// <summary>
        /// 市场价（原价）
        /// </summary>
        public decimal MarketingPrice { get; set; }
        /// <summary>
        /// 总成本价（实物取自库存，服务取自门店）
        /// </summary>
        public decimal TotalCostPrice { get; set; }
        /// <summary>
        /// 服务结算金额(只含服务产品，指乘以购买数量后)
        /// </summary>
        public decimal SettlementAmount { get; set; }
        /// <summary>
        /// 售价
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 总价
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// 库存是否充足
        /// </summary>
        public bool EnoughStock { get; set; }

        /// <summary>
        /// 总数量
        /// </summary>
        public int TotalNumber { get; set; }

        /// <summary>
        /// 分摊优惠金额（指乘以购买数量后）
        /// </summary>
        public decimal ShareDiscountAmount { get; set; }
        /// <summary>
        /// 实际支付金额（指乘以购买数量后）
        /// </summary>
        public decimal ActualPayAmount { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
