using Ae.B.Product.Api.Client.Model.ShopProduc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Request.ShopProduct
{
    public class ShopProductEditClientRequest
    {
        /// <summary>
        /// 商品信息
        /// </summary>
        public BaseShopProductDto ShopProduct { get; set; }
        /// <summary>
        /// 图片信息
        /// </summary>
        public List<string> Images { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        /// <returns></returns>
        public string UserId { get; set; }

        /// <summary>
        /// 是否编辑
        /// </summary>
        public bool IsEdit { get; set; }
    }
}
