using Ae.Product.Service.Core.Model.ShopProduct;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Core.Request.ShopProduct
{
    public class ImportShopProductRequest
    {
        /// <summary>
        /// 门店商品信息
        /// </summary>
        public List<ShopProductImportVo> Products { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        /// <returns></returns>
        public string UserId { get; set; }
    }
}
