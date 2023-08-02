using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Core.Model.Config
{
    /// <summary>
    /// ConfigFrontCategoryVo
    /// </summary>
    public class ConfigFrontCategoryVo
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 分类
        /// </summary>
        public string Category { get; set; } = string.Empty;

        /// <summary>
        /// 显示
        /// </summary>
        public string DisplayName { get; set; } = string.Empty;

        /// <summary>
        /// 展示图url
        /// </summary>
        public string ImageUrl { get; set; } = string.Empty;

        /// <summary>
        /// 跳转路由url
        /// </summary>
        public string RouteUrl { get; set; } = string.Empty;

        /// <summary>
        /// 二级标题
        /// </summary>
        public string SubTitle { get; set; } = string.Empty;

        /// <summary>
        /// 父级Id
        /// </summary>
        public long ParentId { get; set; }

        /// <summary>
        /// 终端类型
        /// </summary>
        public string TerminalType { get; set; } = string.Empty;

        /// <summary>
        /// 排序
        /// </summary>
        public int Rank { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// 扩展参数
        /// </summary>
        public string ExtendParam { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);

        /// <summary>
        /// 更新人
        /// </summary>
        public string UpdateBy { get; set; } = string.Empty;

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);
    }
}
