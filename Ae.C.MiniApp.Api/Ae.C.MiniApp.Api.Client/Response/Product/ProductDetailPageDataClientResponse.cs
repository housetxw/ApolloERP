using Ae.C.MiniApp.Api.Client.Model.BaoYang;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Response.Product
{
    public class ProductDetailPageDataClientResponse
    {
        /// <summary>
        /// 商品数据
        /// </summary>
        public ProductDataDto ProductData { get; set; }

        /// <summary>
        /// 门店信息
        /// </summary>
        public ShopInfoDto ShopInfo { get; set; }

        /// <summary>
        /// 促销信息
        /// </summary>
        public ProductPromotionDto PromotionInfo { get; set; }
    }

    public class ProductDataDto
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
        public List<TagInfoDto> Tags { get; set; } = new List<TagInfoDto>();
    }

    public class ShopInfoDto
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

    public class ProductPromotionDto
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
}
