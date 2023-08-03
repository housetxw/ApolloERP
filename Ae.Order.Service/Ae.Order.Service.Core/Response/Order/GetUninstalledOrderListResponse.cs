﻿using Ae.Order.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Order.Service.Core.Response
{
    public class GetUninstalledOrderListResponse
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 订单状态显示名
        /// </summary>
        public string OrderStatusDisplayName { get; set; }
        /// <summary>
        /// 实付款
        /// </summary>
        public decimal ActualAmount { get; set; }
        /// <summary>
        /// 未安装订单商品集合
        /// </summary>
        public List<UninstalledOrderProductDTO> UninstalledOrderProducts { get; set; }
    }
}
