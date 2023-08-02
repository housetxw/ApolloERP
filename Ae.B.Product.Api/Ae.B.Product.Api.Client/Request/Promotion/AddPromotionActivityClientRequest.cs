using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Request.Promotion
{
    /// <summary>
    /// 新增促销活动
    /// </summary>
    public class AddPromotionActivityClientRequest
    {
        /// <summary>
        /// 活动名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 促销语
        /// </summary>
        public string Subtitle { get; set; }

        /// <summary>
        /// 活动描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 缩略图
        /// </summary>
        public string ThumbUrl { get; set; }

        /// <summary>
        /// 活动类型（0门店商品促销）
        /// </summary>
        public int ActivityType { get; set; }

        /// <summary>
        /// 促销类型（0自定义价格 1打折）
        /// </summary>
        public int PromotionType { get; set; }

        /// <summary>
        /// 标签展示
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// 活动开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 活动结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 适用渠道
        /// </summary>
        public List<string> ApplyChannel { get; set; }

        /// <summary>
        /// 门店
        /// </summary>
        public List<int> ShopIds { get; set; }

        /// <summary>
        /// 促销商品
        /// </summary>
        public List<ActivityProductClientRequest> ActivityProduct { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; }
    }

    /// <summary>
    /// 促销商品
    /// </summary>
    public class ActivityProductClientRequest
    {
        /// <summary>
        /// 商品Pid
        /// </summary>
        public string ProductCode { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 促销价
        /// </summary>
        public decimal PromotionPrice { get; set; }

        /// <summary>
        /// 限购数量
        /// </summary>
        public int LimitQuantity { get; set; }
    }
}
