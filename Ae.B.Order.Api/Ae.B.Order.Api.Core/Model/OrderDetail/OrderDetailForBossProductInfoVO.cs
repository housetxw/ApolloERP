using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Order.Api.Core.Model
{
    public class OrderDetailForBossProductInfoVO
    {
        /// <summary>
        /// 套餐或单品或单服务信息集合
        /// </summary>
        public List<OrderDetailForBossPackageProductVO> Products { get; set; }
    }
}
