using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Model.Order
{
    public class OrderDetailForBossProductInfoDto
    {
        /// <summary>
        /// 套餐或单品或单服务信息集合
        /// </summary>
        public List<OrderDetailForBossPackageProductDto> Products { get; set; }
    }
}
