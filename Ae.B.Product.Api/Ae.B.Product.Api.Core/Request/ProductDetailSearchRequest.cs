using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Request
{
    public class ProductDetailSearchRequest
    {
        /// <summary>
        /// 产品编码
        /// </summary>
        public List<string> ProductCodes { get; set; }
    }
}
