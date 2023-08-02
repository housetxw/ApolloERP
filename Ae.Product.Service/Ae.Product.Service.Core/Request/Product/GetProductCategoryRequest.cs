using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Core.Request.Product
{
    /// <summary>
    /// GetProductCategoryRequest
    /// </summary>
    public class GetProductCategoryRequest
    {
        /// <summary>
        /// 门店ID
        /// </summary>
        public long ShopId { get; set; } = 0;
        /// <summary>
        /// 一级类目
        /// </summary>
        public List<int> MainCategoryId { get; set; }
    }
}
