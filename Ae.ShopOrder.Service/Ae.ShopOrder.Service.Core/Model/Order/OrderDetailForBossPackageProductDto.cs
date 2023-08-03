using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Model.Order
{
    public class OrderDetailForBossPackageProductDto : OrderDetailForBossProductDto
    {

        /// <summary>
        /// 套餐明细（仅当外层ProductAttribute为1时此集合有数据）
        /// </summary>
        public List<OrderDetailForBossProductDto> Children { get; set; } = new List<OrderDetailForBossProductDto>();
    }
}
