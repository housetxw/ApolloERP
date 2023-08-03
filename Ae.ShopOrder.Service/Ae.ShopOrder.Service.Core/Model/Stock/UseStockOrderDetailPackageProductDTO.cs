using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Model.Stock
{

    /// <summary>
    /// 订单明细套餐或单商品（服务）信息
    /// </summary>
    public class UseStockOrderDetailPackageProductDTO
    {
        /// <summary>
        /// 套餐或单商品（服务）信息
        /// </summary>
        public UseStockOrderDetailProductDTO PackageOrProduct { get; set; }

        /// <summary>
        /// 套餐包含的产品集合（仅当PackageOrProduct.ProductAttribute为1时此集合有数据）
        /// </summary>
        public List<UseStockOrderDetailProductDTO> PackageProducts { get; set; }
    }
}
