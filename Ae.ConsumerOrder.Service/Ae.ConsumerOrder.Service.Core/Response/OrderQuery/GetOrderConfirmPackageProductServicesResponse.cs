using Ae.ConsumerOrder.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ConsumerOrder.Service.Core.Response
{
    public class GetOrderConfirmPackageProductServicesResponse
    {
        /// <summary>
        /// 返回的选购套餐或单品集合
        /// </summary>
        public List<OrderConfirmPackageProductDTO> Products { get; set; }
        /// <summary>
        /// 返回选购的商品带出的服务集合
        /// </summary>
        public List<OrderConfirmProductDTO> Services { get; set; }
    }
}
