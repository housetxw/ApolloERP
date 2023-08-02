using System.Collections.Generic;

namespace Ae.B.Product.Api.Client.Model
{
    /// <summary>
    /// 一级类目
    /// </summary>
    public class CategoryTreeSelectDto : TreeSelectDto
    {
        /// <summary>
        /// 子集
        /// </summary>
        public List<SecondCategoryTreeSelectVo> Children { get; set; }
    }

    /// <summary>
    /// 二级类目
    /// </summary>
    public class SecondCategoryTreeSelectVo : TreeSelectDto
    {
        /// <summary>
        /// 子集
        /// </summary>
        public List<ThirdCategoryTreeSelectVo> Children { get; set; }
    }

    /// <summary>
    /// 三级类目
    /// </summary>
    public class ThirdCategoryTreeSelectVo : TreeSelectDto
    {
    }
}
