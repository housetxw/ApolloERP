using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Core.Request.Config
{
    /// <summary>
    /// PackageCardProductPageListRequest
    /// </summary>
    public class PackageCardProductPageListRequest : BasePageRequest
    {
        /// <summary>
        /// 套餐类型
        /// </summary>
        public List<int> Type { get; set; }

        /// <summary>
        /// 是否上架
        /// </summary>
        public sbyte? OnSale { get; set; }

        /// <summary>
        /// 是否零售
        /// </summary>
        public sbyte? IsRetail { get; set; }

        /// <summary>
        /// 关键字
        /// </summary>
        public string Key { get; set; }
    }
}
