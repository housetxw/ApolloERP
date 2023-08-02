using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Core.Model
{
    /// <summary>
    /// 轮胎适配页
    /// </summary>
    public class TireCategoryVO
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
        public List<TagInfo> Tags { get; set; }

        /// <summary>
        /// 商品信息
        /// </summary>
        public List<TireProductVO> Products { get; set; }
    }
}
