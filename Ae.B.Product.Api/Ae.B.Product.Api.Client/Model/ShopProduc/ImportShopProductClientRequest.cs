using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Model.ShopProduc
{
    public class ImportShopProductClientRequest
    {
        /// <summary>
        /// 门店商品信息
        /// </summary>
        public List<ShopProductImportDto> Products { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        /// <returns></returns>
        public string UserId { get; set; }
    }
}
