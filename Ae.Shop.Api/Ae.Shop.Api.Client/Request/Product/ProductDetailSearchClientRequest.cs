using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Client.Request
{
    public class ProductDetailSearchClientRequest
    {
        /// <summary>
        /// 产品编码
        /// </summary>
        public List<string> ProductCodes { get; set; }
    }
}
