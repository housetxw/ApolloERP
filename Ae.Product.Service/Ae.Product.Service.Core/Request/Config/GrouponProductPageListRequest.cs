using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Core.Request.Config
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
