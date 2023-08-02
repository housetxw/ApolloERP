using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Order.Api.Core.Model
{
    public class OrderDetailForBossPackageProductVO : OrderDetailForBossProductVO
    {
        /// <summary>
        /// 套餐明细（仅当外层ProductAttribute为1时此集合有数据）
        /// </summary>
        public List<OrderDetailForBossProductVO> Children { get; set; }
    }
}
