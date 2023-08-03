using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Enums
{
    /// <summary>
    /// 排序枚举值
    /// </summary>
    public enum ProductSearchSortType
    {
        /// <summary>
        /// 评分从低到高
        /// </summary>
        [Description("评分从低到高")]
        ScoreAsc = 1,
        /// <summary>
        /// 评分从高到低
        /// </summary>
        [Description("评分从高到低")]
        ScoreDesc = 2,
        /// <summary>
        /// 销量从低到高
        /// </summary>
        [Description("销量从低到高")]
        SalesAsc = 3,
        /// <summary>
        /// 销量从高到低
        /// </summary>
        [Description("销量从高到低")]
        SalesDesc = 4,
        /// <summary>
        /// 价格从低到高
        /// </summary>
        [Description("价格从低到高")]
        PriceAsc = 5,

        /// <summary>
        /// 价格从高到低
        /// </summary>
        [Description("价格从高到低")]
        PriceDesc = 6
    }
}
