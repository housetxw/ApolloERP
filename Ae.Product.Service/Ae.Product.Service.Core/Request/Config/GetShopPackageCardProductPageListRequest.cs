using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Core.Request.Config
{
    public class GetShopPackageCardProductPageListRequest: BasePageRequest
    {
        /// <summary>
        /// 关键字
        /// </summary>
        public string Key { get; set; }

        public long ShopId { get; set; }
    }
}
