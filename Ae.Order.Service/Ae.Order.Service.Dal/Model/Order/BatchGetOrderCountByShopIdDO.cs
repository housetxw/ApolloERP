﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Order.Service.Dal.Model.Order
{
    public class BatchGetOrderCountByShopIdDO
    {
        /// <summary>
        /// 门店ID
        /// </summary>
        public long ShopId { get; set; }
        /// <summary>
        /// 订单总数
        /// </summary>
        public int OrderCount { get; set; }
    }
}
