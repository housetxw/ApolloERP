using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Model.Product
{
    /// <summary>
    /// GrouponProductVo
    /// </summary>
    public class GrouponProductVo : ProductBaseInfoVo
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long GrouponId { get; set; }

        /// <summary>
        /// 最小价格
        /// </summary>
        public decimal MinPrice { get; set; }

        /// <summary>
        /// 最大价格
        /// </summary>
        public decimal MaxPrice { get; set; }
    }
}
