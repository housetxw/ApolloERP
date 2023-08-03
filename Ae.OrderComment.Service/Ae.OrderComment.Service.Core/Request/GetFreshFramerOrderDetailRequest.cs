using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.OrderComment.Service.Core.Request
{
    public class GetFreshFramerOrderDetailRequest
    {
        public string OrderNo { get; set; }
    }

    public class GetFreshFramerOrderListResponse
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }


        /// <summary>
        /// 显示订单状态（结合多个状态计算产生）
        /// </summary>
        public string DisplayOrderStatus { get; set; }
        /// <summary>
        /// 实付款
        /// </summary>
        public decimal ActualAmount { get; set; }

        public long ShopId { get; set; }

        public string ShopName { get; set; }

        /// <summary>
        /// 商品信息集合
        /// </summary>
        public List<OrderListProduct> Products { get; set; }
        /// <summary>
        /// 订单用户当前可执行操作集合
        /// </summary>
    }

    public class OrderListProduct
    {
        public int Id { get; set; }
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
        /// 图片路径
        /// </summary>
        public string ImageUrl { get; set; } = string.Empty;
        /// <summary>
        /// 市场单价
        /// </summary>
        public decimal MarketingPrice { get; set; }
        /// <summary>
        /// 销售单价
        /// </summary>
        public decimal Price { get; set; }
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
        /// 产品规格
        /// </summary>
        public string Specifications { get; set; }
    }

}
