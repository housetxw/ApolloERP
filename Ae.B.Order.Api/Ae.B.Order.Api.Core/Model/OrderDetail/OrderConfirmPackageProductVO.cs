using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Order.Api.Core.Model.OrderDetail
{
    /// <summary>
    /// 订单确认页套餐或单品信息
    /// </summary>
    public class OrderConfirmPackageProductVO
    {
        /// <summary>
        /// 套餐或单品信息
        /// </summary>
        public OrderConfirmProductVO PackageOrProduct { get; set; }

        /// <summary>
        /// 套餐包含的产品集合（仅当PackageOrProduct.ProductAttribute为1时此集合有数据）
        /// </summary>
        public List<OrderConfirmProductVO> PackageProducts { get; set; }
    }
}
