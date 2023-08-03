using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Response.Adaptation
{
    /// <summary>
    /// 筛选集
    /// </summary>
    public class FilterCategory
    {
        /// <summary>
        /// 类型 Brand
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 中文显示 品牌
        /// </summary>
        public string ZhName { get; set; }

        /// <summary>
        /// 是否允许多选
        /// </summary>
        public bool IsMultiSelect { get; set; }

        /// <summary>
        /// 筛选值
        /// </summary>
        public IEnumerable<ConditionModel> Values { get; set; }
    }

    /// <summary>
    /// 条件
    /// </summary>
    public class ConditionModel
    {
        /// <summary>
        /// 状态 是否置灰可选
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string Image { get; set; }
    }
}
