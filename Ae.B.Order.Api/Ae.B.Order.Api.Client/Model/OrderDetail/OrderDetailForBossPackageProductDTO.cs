using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Order.Api.Client.Model
{
    public class OrderDetailForBossPackageProductDTO : OrderDetailForBossProductDTO
    {
        /// <summary>
        /// 套餐明细（仅当外层ProductAttribute为1时此集合有数据）
        /// </summary>
        public List<OrderDetailForBossProductDTO> Children { get; set; }
    }
}
