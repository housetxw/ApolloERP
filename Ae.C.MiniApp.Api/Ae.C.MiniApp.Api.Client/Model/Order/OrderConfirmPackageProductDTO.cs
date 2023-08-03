using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Model.Order
{
    /// <summary>
    /// 订单确认页套餐或单品信息
    /// </summary>
    public class OrderConfirmPackageProductDTO
    {
        /// <summary>
        /// 套餐或单品信息
        /// </summary>
        public OrderConfirmProductDTO PackageOrProduct { get; set; }

        /// <summary>
        /// 套餐包含的产品集合（仅当PackageOrProduct.ProductAttribute为1时此集合有数据）
        /// </summary>
        public List<OrderConfirmProductDTO> PackageProducts { get; set; }
    }
}
