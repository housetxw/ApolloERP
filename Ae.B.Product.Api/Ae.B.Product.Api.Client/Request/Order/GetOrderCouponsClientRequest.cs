using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Request.Order
{
    public class GetOrderCouponsClientRequest
    {
        public List<long> CouponIds { get; set; }
    }
}
