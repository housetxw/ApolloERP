using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Core.Model.Category
{
    /// <summary>
    /// ProductCategoryVo
    /// </summary>
    public class ProductCategoryVo
    {
        /// <summary>
        /// 类目Id
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// 类目Code缩写
        /// </summary>
        public string CategoryCodeShort { get; set; } = string.Empty;

        /// <summary>
        /// 显示名
        /// </summary>
        public string DisplayName { get; set; } = string.Empty;

        /// <summary>
        /// 类型基本 一级 二级 三级
        /// </summary>
        public int CategoryLevel { get; set; }

        /// <summary>
        /// 父级Id
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 子集类目
        /// </summary>
        public List<ProductCategoryVo> Children { get; set; }
    }
}
