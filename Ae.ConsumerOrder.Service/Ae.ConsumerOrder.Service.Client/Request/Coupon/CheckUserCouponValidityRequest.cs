using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ConsumerOrder.Service.Client.Request.Coupon
{
    public  class CheckUserCouponValidityRequest
    {
        public long UserCouponId { get; set; }

        public string UserId { get; set; }
    }
}
