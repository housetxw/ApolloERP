using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Model.ShopProduc
{
    public class ShopProductDto:BaseShopProductDto
    {
        /// <summary>
        /// 图片信息
        /// </summary>
        public List<string> Images { get; set; }
    }
}
