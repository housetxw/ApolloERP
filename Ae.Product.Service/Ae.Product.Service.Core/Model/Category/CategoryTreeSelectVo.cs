using System.Collections.Generic;

namespace Ae.Product.Service.Core.Model
{
    /// <summary>
    /// 一级类目
    /// </summary>
    public class CategoryTreeSelectVo : TreeSelectVo
    {
        /// <summary>
        /// 子集
        /// </summary>
        public List<SecondCategoryTreeSelectVo> Children { get; set; }
    }

    /// <summary>
    /// 二级类目
    /// </summary>
    public class SecondCategoryTreeSelectVo : TreeSelectVo
    {
        /// <summary>
        /// 子集
        /// </summary>
        public List<ThirdCategoryTreeSelectVo> Children { get; set; }
    }

    /// <summary>
    /// 三级类目
    /// </summary>
    public class ThirdCategoryTreeSelectVo : TreeSelectVo
    {
    }
}
