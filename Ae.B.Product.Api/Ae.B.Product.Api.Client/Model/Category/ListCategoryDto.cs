using System.Collections.Generic;

namespace Ae.B.Product.Api.Client.Model
{
    /// <summary>
    /// 一级类目
    /// </summary>
    public class ListCategoryDto : DimCategoryDto
    {
        /// <summary>
        /// 子集
        /// </summary>
        public List<SecondCategoryDto> Children { get; set; } = new List<SecondCategoryDto>();
    }

    /// <summary>
    /// 二级类目
    /// </summary>
    public class SecondCategoryDto : DimCategoryDto
    {
        /// <summary>
        /// 子集
        /// </summary>
        public List<ThirdCategoryDto> Children { get; set; } = new List<ThirdCategoryDto>();
    }

    /// <summary>
    /// 三级类目
    /// </summary>
    public class ThirdCategoryDto : DimCategoryDto
    {
    }

}
