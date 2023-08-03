using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ConsumerOrder.Service.Client.Request
{
    public class UpdateUserCouponStatusReqByIdDTO
    {
        public long UserCouponId { get; set; }
        public string UserId { get; set; }
    }
}
