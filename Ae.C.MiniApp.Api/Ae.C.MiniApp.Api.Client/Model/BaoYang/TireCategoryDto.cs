using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Model.BaoYang
{
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
}
