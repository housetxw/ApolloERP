using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ConsumerOrder.Service.Core.Model
{
    public class OrderDetailForBossPackageProductDTO : OrderDetailForBossProductDTO
    {
        /// <summary>
        /// 套餐明细（仅当外层ProductAttribute为1时此集合有数据）
        /// </summary>
        public List<OrderDetailForBossProductDTO> Children { get; set; } = new List<OrderDetailForBossProductDTO>();
    }
}
