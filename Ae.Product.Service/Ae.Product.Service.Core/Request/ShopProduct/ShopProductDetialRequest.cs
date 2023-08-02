using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Core.Request.ShopProduct
{
    public class ShopProductDetialRequest
    {
        /// <summary>
        /// 门店商品编码
        /// </summary>
        public List<string> ShopProductCodes { get; set; }
    }
}
