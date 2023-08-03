using System;
using System.Collections.Generic;
using System.Text;
using Ae.ShopOrder.Service.Core.Model.Order;

namespace Ae.ShopOrder.Service.Core.Response.Order
{
    public class GetOrderDetailPackageProductServicesResponse
    {
        /// <summary>
        /// 套餐或单品信息集合
        /// </summary>
        public List<OrderDetailPackageProductDTO> Products { get; set; } = new List<OrderDetailPackageProductDTO>();
        /// <summary>
        /// 服务信息集合
        /// </summary>
        public List<OrderDetailProductDTO> Services { get; set; } = new List<OrderDetailProductDTO>();
    }
}
