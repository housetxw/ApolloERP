using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Request.Product
{
    public class GetShopPackageCardProductPageListRequest : BasePageRequest
    {
        /// <summary>
        /// 关键字
        /// </summary>
        public string Key { get; set; }

        public long ShopId { get; set; }
    }
}
