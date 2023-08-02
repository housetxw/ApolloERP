using Ae.Product.Service.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Core.Model.Config
{
    /// <summary>
    /// FrontCategoryVo
    /// </summary>
    public class FrontCategoryVo
    {
        /// <summary>
        /// id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 分类code
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 图片url
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 跳转路由
        /// </summary>
        public string RouteUrl { get; set; }

        /// <summary>
        /// 二级标题
        /// </summary>
        public string SubTitle { get; set; }

        /// <summary>
        /// 终端类型
        /// </summary>
        public TerminalType TerminalType { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Rank { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// 扩展参数
        /// </summary>
        public string ExtendParam { get; set; }

        /// <summary>
        /// 子集
        /// </summary>
        public List<FrontCategoryVo> Children { get; set; }
    }
}
