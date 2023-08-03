using System;
using System.Collections.Generic;
using System.Text;
using Ae.ShopOrder.Service.Core.Enums;

namespace Ae.ShopOrder.Service.Client.Request.Order
{
    /// <summary>
    /// 业务状态搜索订单请求对象
    /// </summary>
    public class GetOrderBaseInfoForBusinessStatusRequest
    {
        /// <summary>
        /// 订单状态
        /// </summary>
        public OrderBusinessStatusEnum OrderBusinessStatus { get; set; }

        /// <summary>
        /// 门店Id
        /// </summary>
        public int ShopId { get; set; }
    }
}
