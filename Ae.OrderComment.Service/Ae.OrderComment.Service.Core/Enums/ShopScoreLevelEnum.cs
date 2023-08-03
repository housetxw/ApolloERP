using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.OrderComment.Service.Core.Enums
{
    /// <summary>
    /// 门店评分枚举
    /// </summary>
    public enum ShopScoreLevelEnum
    {
        /// <summary>
        /// 0全部
        /// </summary>
        [Description("全部")] All = 0,

        // <summary>
        /// 1好评 (5)
        /// </summary>
        [Description("好评")] ShopGood = 4,

        // <summary>
        /// 1差评 (3分及以下)
        /// </summary>
        [Description("差评")] ShopNegative = 3
    }
}
