using Ae.ConsumerOrder.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ConsumerOrder.Service.Core.Response
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
