using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Client.Response.Order
{
    public class OrderProductDTO
    {
        /// <summary>
        /// 物理主键（不用于对外逻辑和关联）
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 平台订单Id
        /// </summary>
        public long OrderId { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; } = string.Empty;
        /// <summary>
        /// 子系统订单商品Id（同步关联）
        /// </summary>
        public long OrderProductId { get; set; }
        /// <summary>
        /// 产品Id
        /// </summary>
        public string ProductId { get; set; } = string.Empty;
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; } = string.Empty;
        /// <summary>
        /// 产品显示名称
        /// </summary>
        public string DisplayName { get; set; } = string.Empty;
        /// <summary>
        /// 产品描述
        /// </summary>
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// 产品性质（0 实物产品、1 套餐产品、2 服务产品、3 数字产品）
        /// </summary>
        public sbyte ProductAttribute { get; set; }
        /// <summary>
        /// 所属父级订单套餐产品Id
        /// </summary>
        public long ParentOrderPackagePid { get; set; }
        /// <summary>
        /// 服务属于订单实物产品Id（多个pid以;分割)
        /// </summary>
        public string ServeForOrderPids { get; set; } = string.Empty;
        /// <summary>
        /// 商品类目编号
        /// </summary>
        public string CategoryCode { get; set; } = string.Empty;
        /// <summary>
        /// 适配项目编号（暂不存储）
        /// </summary>
        public string ItemCode { get; set; } = string.Empty;
        /// <summary>
        /// 标签
        /// </summary>
        public string Label { get; set; } = string.Empty;
        /// <summary>
        /// 图片路径
        /// </summary>
        public string ImageUrl { get; set; } = string.Empty;
        /// <summary>
        /// 价格Id
        /// </summary>
        public long PriceId { get; set; }
        /// <summary>
        /// 市场单价
        /// </summary>
        public decimal MarketingPrice { get; set; }
        /// <summary>
        /// 销售单价
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 总成本价（实物取自库存，服务取自门店）（指乘以购买数量后）
        /// </summary>
        public decimal TotalCostPrice { get; set; }
        /// <summary>
        /// 单位（个/升/斤等）
        /// </summary>
        public string Unit { get; set; } = string.Empty;
        /// <summary>
        /// 数量（指单个套餐中该商品）
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// 总数量（指乘以购买数量后）
        /// </summary>
        public int TotalNumber { get; set; }
        /// <summary>
        /// 商品库存状态（0未占库 1占库中 2已占库 3释放中 4已释放）
        /// </summary>
        public sbyte StockStatus { get; set; }
        /// <summary>
        /// 金额（指单个套餐中该商品）
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// 总金额（指乘以购买数量后）
        /// </summary>
        public decimal TotalAmount { get; set; }
        /// <summary>
        /// 税率
        /// </summary>
        public decimal TaxRate { get; set; }
        /// <summary>
        /// 分摊优惠金额（指乘以购买数量后）
        /// </summary>
        public decimal ShareDiscountAmount { get; set; }
        /// <summary>
        /// 实际支付金额（指乘以购买数量后）
        /// </summary>
        public decimal ActualPayAmount { get; set; }
        /// <summary>
        /// 删除标识（0否 1是）
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
