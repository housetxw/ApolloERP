using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Request.Product
{
    /// <summary>
    /// GrouponProductPageListRequest
    /// </summary>
    public class GrouponProductPageListRequest : BasePageRequest
    {
        /// <summary>
        /// 是否上架
        /// </summary>
        public sbyte? OnSale { get; set; }

        /// <summary>
        /// 关键字
        /// </summary>
        public string Key { get; set; }
    }
}
