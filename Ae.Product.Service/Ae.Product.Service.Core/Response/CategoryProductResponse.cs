using Ae.Product.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Core.Response
{
    public class CategoryProductResponse
    {
        /// <summary>
        /// 商品信息
        /// </summary>
        public List<ProductSearchInfoVo> products { get; set; }

        /// <summary>
        /// 总数量
        /// </summary>
        public int TotalCount { get; set; }
    }
}
