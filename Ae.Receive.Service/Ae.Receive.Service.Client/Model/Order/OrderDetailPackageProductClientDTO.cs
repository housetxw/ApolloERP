using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Client.Model
{
    public class OrderDetailPackageProductClientDTO
    {
        /// <summary>
        /// 套餐或单品（服务）信息
        /// </summary>
        public OrderDetailProductClientDTO PackageOrProduct { get; set; }

        /// <summary>
        /// 套餐包含的产品集合（仅当PackageOrProduct.ProductAttribute为1时此集合有数据）
        /// </summary>
        public List<OrderDetailProductClientDTO> PackageProducts { get; set; }
    }
}
