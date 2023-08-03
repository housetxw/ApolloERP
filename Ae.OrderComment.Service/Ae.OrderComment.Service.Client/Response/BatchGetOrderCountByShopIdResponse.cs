﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.OrderComment.Service.Client.Response
{
    public class BatchGetOrderCountByShopIdResponse
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
