using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Core.Response
{
    /// <summary>
    /// 商品详情页数据
    /// </summary>
    public class ProductDetailPageDataResponse
    {
        /// <summary>
        /// 商品数据
        /// </summary>
        public ProductDataVo ProductData { get; set; }

        /// <summary>
        /// 门店信息
        /// </summary>
        public ShopInfoVo ShopInfo { get; set; }

        /// <summary>
        /// 促销信息
        /// </summary>
        public ProductPromotionVo PromotionInfo { get; set; }
    }

    /// <summary>
    /// 商品信息
    /// </summary>
    public class ProductDataVo
    {
        /// <summary>
        /// 商品Pid
        /// </summary>
        public string ProductCode { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 产品显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// 基准价格
        /// </summary>
        public decimal StandardPrice { get; set; }

        /// <summary>
        /// 销售价
        /// </summary>
        public decimal SalesPrice { get; set; }

        /// <summary>
        ///  单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        ///  上下架 
        /// </summary>
        public int OnSale { get; set; }

        /// <summary>
        /// 是否缺货  1 是 0否
        /// </summary>
        public int StockOut { get; set; }

        /// <summary>
        /// 是否零售
        /// </summary>
        public int IsRetail { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public List<string> Images { get; set; } = new List<string>();

        /// <summary>
        /// 描述 富文本
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 服务项目描述
        /// </summary>
        public string Detail { get; set; }

        /// <summary>
        /// 服务项目描述图片
        /// </summary>
        public List<string> DetailImages { get; set; } = new List<string>();

        /// <summary>
        /// 标签
        /// </summary>
        public List<TagInfo> Tags { get; set; } = new List<TagInfo>();
    }

    /// <summary>
    /// 门店信息
    /// </summary>
    public class ShopInfoVo
    {
        /// <summary>
        /// 简单名
        /// </summary>
        public string SimpleName { get; set; }

        /// <summary>
        /// 门店地址
        /// </summary>
        public string Address { get; set; }
    }

    /// <summary>
    /// 促销信息
    /// </summary>
    public class ProductPromotionVo
    {
        /// <summary>
        /// 可售数量
        /// </summary>
        public int AvailQuantity { get; set; }

        /// <summary>
        /// 促销活动Id
        /// </summary>
        public string ActivityId { get; set; }

        /// <summary>
        /// 活动开始时间
        /// </summary>
        public string StartTime { get; set; }

        /// <summary>
        /// 活动结束时间
        /// </summary>
        public string EndTime { get; set; }

        /// <summary>
        /// 促销价
        /// </summary>
        public decimal PromotionPrice { get; set; }
    }

    /// <summary>
    /// 标签
    /// </summary>
    public class TagInfo
    {
        /// <summary>
        /// 标签CODE(EG:RGRecommend)
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 标签名称(EG:总部推荐)
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// 标签颜色(EG:#F37C3E)
        /// </summary>
        public string TagColor { get; set; }
    }
}
