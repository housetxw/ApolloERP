using Ae.B.Product.Api.Core.Model.ShopProduct;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Response.ShopProduct
{
    public class ShopProductSearchResponse
    {
        /// <summary>
        /// 商品信息
        /// </summary>
        public List<ShopProductVo> Items { get; set; }

        /// <summary>
        /// 总数量
        /// </summary>
        public int TotalItems { get; set; }
    }
}
