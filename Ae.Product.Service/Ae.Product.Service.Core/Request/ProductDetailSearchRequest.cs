using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Core.Request
{
    public class ProductDetailSearchRequest
    {
        /// <summary>
        /// 产品编码
        /// </summary>
        public List<string> ProductCodes { get; set; }
    }
}
