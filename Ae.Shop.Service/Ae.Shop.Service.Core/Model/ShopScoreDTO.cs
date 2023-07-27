﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Model
{
    public class ShopScoreDTO
    {
        /// <summary>
        /// 门店ID
        /// </summary>
        public long ShopId { get; set; }
        /// <summary>
        /// 总评论数
        /// </summary>
        public decimal Score { get; set;}
        /// <summary>
        /// 总订单数
        /// </summary>
        public int TotalOrder { get; set; }
        /// <summary>
        /// 好评率（单位为%）
        /// </summary>
        public decimal ApplauseRate { get; set; }
    }
}
