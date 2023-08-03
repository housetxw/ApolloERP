using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ConsumerOrder.Service.Core.Request.SharingPromotion
{
    public class GetSharingCouponRequest : BaseGetRequest
    {
        public string UserId { get; set; }
    }
}
