using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ConsumerOrder.Service.Core.Model
{
    /// <summary>
    /// 订单明细套餐或单品信息
    /// </summary>
    public class OrderDetailPackageProductDTO
    {
        /// <summary>
        /// 套餐或单品（服务）信息
        /// </summary>
        public OrderDetailProductDTO PackageOrProduct { get; set; }

        /// <summary>
        /// 套餐包含的产品集合（仅当PackageOrProduct.ProductAttribute为1时此集合有数据）
        /// </summary>
        public List<OrderDetailProductDTO> PackageProducts { get; set; }
    }
}
