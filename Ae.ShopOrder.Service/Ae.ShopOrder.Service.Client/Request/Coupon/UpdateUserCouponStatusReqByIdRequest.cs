using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Client.Request.Coupon
{
    public class UpdateUserCouponStatusReqByIdRequest
    {
        public long UserCouponId { get; set; }
        public string UserId { get; set; }
    }
}
