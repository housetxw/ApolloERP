using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.OrderComment.Service.Client.Request
{
    public class GetFreshFramerOrderDetailRequest
    {
        public string OrderNo { get; set; }
    }

    public class GetFreshFramerOrderDetailResponse
    {
        public string ReceiveId { get; set; }
        public string Receiver { get; set; }

        public string ReceiverPhone { get; set; }

        public string ReceiverAddress { get; set; }

        public long ShopId { get; set; }
        public int OrderChannel { get; set; }

        public int OrderType { get; set; }

        public string ShopName { get; set; }

        public List<FreshFramerProduct> Products { get; set; }

        /// <summary>
        /// 运费
        /// </summary>
        public Decimal Freight { get; set; }

        /// <summary>
        /// 总价
        /// </summary>
        public Decimal TotalAmount { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CrateTime { get; set; }

        /// <summary>
        /// 付款方式
        /// </summary>
        public string ShowPayType { get; set; }

        /// <summary>
        /// 付款时间
        /// </summary>
        public string PayTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }



        public bool IsShowTraceCode { get; set; }

        /// <summary>
        /// 视频的地址
        /// </summary>
        public string VideoImg { get; set; }

        public string ShowOrderStatus { get; set; }
    }

    public class FreshFramerProduct
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
        /// 总价
        /// </summary>
        public decimal TotalAmount { get; set; }


        /// <summary>
        /// 产品规格
        /// </summary>
        public string Specifications { get; set; }


    }

}
