﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Request.Arrival
{
    public  class PayListForMiniRequest
    {
        /// <summary>
        /// 预约记录Id
        /// </summary>
        public long ReserverNo { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// shopId 
        /// </summary>
        public int ShopId { get; set; }
    }
}
