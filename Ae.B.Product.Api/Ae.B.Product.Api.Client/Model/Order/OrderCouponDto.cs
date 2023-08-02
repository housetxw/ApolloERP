using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Model.Order
{
    public class OrderCouponDto
    {
        public long UserCouponId { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 订单Id
        /// </summary>
        public long OrderId { get; set; }
    }
}
