﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Response
{
    public class GetShopServiceListWithPIDResponse
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public string PID { get; set; }
        /// <summary>
        /// 是否上下架
        /// </summary>
        public bool IsOnline { get; set; }
        /// <summary>
        /// 成本价
        /// </summary>
        public decimal CostPrice { get; set; }
    }
}
