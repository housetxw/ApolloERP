using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Core.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class TireProductVo
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
        public List<TagInfo> Tags { get; set; } = new List<TagInfo>();

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
        public IEnumerable<TireProductVo> ChildProducts { get; set; }

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
