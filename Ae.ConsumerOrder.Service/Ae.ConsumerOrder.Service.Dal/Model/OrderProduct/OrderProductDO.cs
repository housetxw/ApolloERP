using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;

namespace Ae.ConsumerOrder.Service.Dal.Model
{
    [Table("order_product")]
    public class OrderProductDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 订单Id
        /// </summary>
        [Column("order_id")]
        public long OrderId { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        [Column("order_no")]
        public string OrderNo { get; set; } = string.Empty;
        /// <summary>
        /// 产品Id
        /// </summary>
        [Column("product_id")]
        public string ProductId { get; set; } = string.Empty;
        /// <summary>
        /// 产品名称
        /// </summary>
        [Column("product_name")]
        public string ProductName { get; set; } = string.Empty;
        /// <summary>
        /// 产品显示名称
        /// </summary>
        [Column("display_name")]
        public string DisplayName { get; set; } = string.Empty;
        /// <summary>
        /// 产品描述
        /// </summary>
        [Column("description")]
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// 产品性质（0 实物产品、1 套餐产品、2 服务产品、3 数字产品）
        /// </summary>
        [Column("product_attribute")]
        public sbyte ProductAttribute { get; set; }
        /// <summary>
        /// 所属父级订单套餐产品Id
        /// </summary>
        [Column("parent_order_package_pid")]
        public long ParentOrderPackagePid { get; set; }
        /// <summary>
        /// 服务属于订单实物产品Id（多个pid以;分割)
        /// </summary>
        [Column("serve_for_order_pids")]
        public string ServeForOrderPids { get; set; } = string.Empty;
        /// <summary>
        /// 商品类目编号
        /// </summary>
        [Column("category_code")]
        public string CategoryCode { get; set; } = string.Empty;
        /// <summary>
        /// 适配项目编号（暂不存储）
        /// </summary>
        [Column("item_code")]
        public string ItemCode { get; set; } = string.Empty;
        /// <summary>
        /// 标签
        /// </summary>
        [Column("label")]
        public string Label { get; set; } = string.Empty;
        /// <summary>
        /// 图片路径
        /// </summary>
        [Column("image_url")]
        public string ImageUrl { get; set; } = string.Empty;
        /// <summary>
        /// 价格Id
        /// </summary>
        [Column("price_id")]
        public long PriceId { get; set; }
        /// <summary>
        /// 市场单价
        /// </summary>
        [Column("marketing_price")]
        public decimal MarketingPrice { get; set; }
        /// <summary>
        /// 销售单价
        /// </summary>
        [Column("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// 总成本价（实物取自库存，服务取自门店）（指乘以购买数量后）
        /// </summary>
        [Column("total_cost_price")]
        public decimal TotalCostPrice { get; set; }
        /// <summary>
        /// 单位（个/升/斤等）
        /// </summary>
        [Column("unit")]
        public string Unit { get; set; } = string.Empty;
        /// <summary>
        /// 数量（指单个套餐中该商品）
        /// </summary>
        [Column("number")]
        public int Number { get; set; }
        /// <summary>
        /// 总数量（指乘以购买数量后）
        /// </summary>
        [Column("total_number")]
        public int TotalNumber { get; set; }
        /// <summary>
        /// 商品库存状态（0未占库 1占库中 2已占库 3释放中 4已释放）
        /// </summary>
        [Column("stock_status")]
        public sbyte StockStatus { get; set; }
        /// <summary>
        /// 金额（指单个套餐中该商品）
        /// </summary>
        [Column("amount")]
        public decimal Amount { get; set; }
        /// <summary>
        /// 总金额（指乘以购买数量后）
        /// </summary>
        [Column("total_amount")]
        public decimal TotalAmount { get; set; }
        /// <summary>
        /// 税率
        /// </summary>
        [Column("tax_rate")]
        public decimal TaxRate { get; set; }
        /// <summary>
        /// 分摊优惠金额（指乘以购买数量后）
        /// </summary>
        [Column("share_discount_amount")]
        public decimal ShareDiscountAmount { get; set; }
        /// <summary>
        /// 实际支付金额（指乘以购买数量后）
        /// </summary>
        [Column("actual_pay_amount")]
        public decimal ActualPayAmount { get; set; }
        /// <summary>
        /// 删除标识（0否 1是）
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
    }
}
