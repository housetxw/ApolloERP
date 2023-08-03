using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Client.Model
{
    public class UseStockOrderDetailPackageProductDTO
    {
        /// <summary>
        /// 套餐或单商品（服务）信息
        /// </summary>
        public UseStockOrderDetailProductDTO PackageOrProduct { get; set; }

        /// <summary>
        /// 套餐包含的产品集合（仅当PackageOrProduct.IsPackageProduct为true时此集合有数据）
        /// </summary>
        public List<UseStockOrderDetailProductDTO> PackageProducts { get; set; }
    }
}
