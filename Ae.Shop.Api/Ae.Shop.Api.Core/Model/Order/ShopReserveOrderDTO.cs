using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Model.Order
{
    public class ShopReserveOrderDTO
    {
        /// <summary>
        /// 预约Id
        /// </summary>
        public long ReserveId { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; } = string.Empty;
    }
}
