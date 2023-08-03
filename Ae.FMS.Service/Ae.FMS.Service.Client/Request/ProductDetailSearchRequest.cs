using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.FMS.Service.Client.Request
{
    public class ProductDetailSearchRequest
    {
        /// <summary>
        /// 产品编码
        /// </summary>
        public List<string> ProductCodes { get; set; }
    }

    public class ShopProductDetialRequest
    {
        /// <summary>
        /// 门店商品编码
        /// </summary>
        public List<string> ShopProductCodes { get; set; }
    }
}
