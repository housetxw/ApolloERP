using Ae.B.Product.Api.Core.Model;
using System.Collections.Generic;

/// <summary>
/// 一级类目
/// </summary>
public class ListCategoryVo : DimCategoryVo
{
    /// <summary>
    /// 子集
    /// </summary>
    public List<SecondCategoryVo> Children { get; set; }
}

/// <summary>
/// 二级类目
/// </summary>
public class SecondCategoryVo : DimCategoryVo
{
    /// <summary>
    /// 子集
    /// </summary>
    public List<ThirdCategoryVo> Children { get; set; }
}

/// <summary>
/// 三级类目
/// </summary>
public class ThirdCategoryVo : DimCategoryVo
{
}
