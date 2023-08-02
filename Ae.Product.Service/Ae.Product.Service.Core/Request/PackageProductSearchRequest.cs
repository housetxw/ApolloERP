using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Core.Request
{
    public class PackageProductSearchRequest
    {
        /// <summary>
        /// 产品编码
        /// </summary>
        public List<string> ProductCodes { get; set; }
    }
}
