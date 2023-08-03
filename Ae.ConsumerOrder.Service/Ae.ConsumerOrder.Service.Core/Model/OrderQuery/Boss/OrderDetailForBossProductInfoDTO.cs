using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ConsumerOrder.Service.Core.Model
{
    public class OrderDetailForBossProductInfoDTO
    {
        /// <summary>
        /// 套餐或单品或单服务信息集合
        /// </summary>
        public List<OrderDetailForBossPackageProductDTO> Products { get; set; }
    }
}
