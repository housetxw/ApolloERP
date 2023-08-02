using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Client.Response.BaoYang
{
    public class TireServiceListResponse
    {
        /// <summary>
        /// 轮胎分类
        /// </summary>
        public List<TireCategoryDto> TireCategory { get; set; }

        /// <summary>
        /// 默认轮胎尺寸
        /// </summary>
        public string DefaultTireSize { get; set; }

        /// <summary>
        /// 更多轮胎尺寸
        /// </summary>
        public List<string> TireSizes { get; set; }
    }

    public class TireCategoryDto
    {
        /// <summary>
        /// 分类
        /// </summary>
        public string CategoryType { get; set; }

        /// <summary>
        /// 中文显示名
        /// </summary>
        public string ZhName { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 项目简单描述
        /// </summary>
        public string BriefDescription { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public List<TagInfoDto> Tags { get; set; }

        /// <summary>
        /// 商品信息
        /// </summary>
        public List<TireProductDto> Products { get; set; }

        /// <summary>
        /// 是否展开
        /// </summary>
        public bool IsDefaultExpand { get; set; }
    }

    public class TagInfoDto
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

    public class TireProductDto
    {
        /// <summary>
        /// 产品ID
        /// </summary>
        public string Pid { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 产品图片
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 市场价
        /// </summary>
        public decimal MarketPrice { get; set; }

        /// <summary>
        /// 适配数量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public List<TagInfoDto> Tags { get; set; } = new List<TagInfoDto>();

        /// <summary>
        /// 标缺
        /// </summary>
        public bool StockOut { get; set; }

        /// <summary>
        /// 是否原配
        /// </summary>
        public bool IsOriginal { get; set; }


        /// <summary>
        /// 是否套餐产品
        /// </summary>
        public bool IsPackageProduct { get; set; }

        /// <summary>
        /// 套餐子产品
        /// </summary>
        public IEnumerable<TireProductDto> ChildProducts { get; set; }

        /// <summary>
        /// 是否关注
        /// </summary>
        public bool IsAttention { get; set; }

        /// <summary>
        /// 是否默认选中
        /// </summary>
        public bool IsDefaultSelect { get; set; }
    }
}
