using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request.SharingPromotion
{
    public class GetSharingCouponRequest:BaseGetRequest
    {
        public string UserId { get; set; }
    }
}
