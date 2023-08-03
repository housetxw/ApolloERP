using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Client.Request.Product
{
    /// <summary>
    /// ProductCategoryClientRequest
    /// </summary>
    public class ProductCategoryClientRequest
    {
        /// <summary>
        /// 一级类目
        /// </summary>
        public List<int> MainCategoryId { get; set; }
    }
}
