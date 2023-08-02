using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Core.Request.ShopProduct
{
    /// <summary>
    /// GrouponProductPageListRequest
    /// </summary>
    public class ShopGrouponProductPageListRequest : BasePageRequest
    {
        /// <summary>
        /// 产品名称或pid
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public sbyte? Status { get; set; }
    }
}
