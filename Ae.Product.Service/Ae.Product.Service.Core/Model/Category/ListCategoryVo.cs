using System.Collections.Generic;

namespace Ae.Product.Service.Core.Model
{
    /// <summary>
    /// 一级类目
    /// </summary>
    public class ListCategoryVo : DimCategoryVo
    {
        /// <summary>
        /// 子集
        /// </summary>
        public List<SecondCategoryVo> Children { get; set; } = new List<SecondCategoryVo>();
    }

    /// <summary>
    /// 二级类目
    /// </summary>
    public class SecondCategoryVo : DimCategoryVo
    {
        /// <summary>
        /// 子集
        /// </summary>
        public List<ThirdCategoryVo> Children { get; set; } = new List<ThirdCategoryVo>();
    }

    /// <summary>
    /// 三级类目
    /// </summary>
    public class ThirdCategoryVo : DimCategoryVo
    {
    }
}
