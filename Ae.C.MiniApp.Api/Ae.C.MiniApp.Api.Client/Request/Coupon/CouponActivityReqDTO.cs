using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Ae.C.MiniApp.Api.Core.Model.Coupon;
using Ae.C.MiniApp.Api.Core.Request;

namespace Ae.C.MiniApp.Api.Client.Request.Coupon
{
    public class CouponActivityReqDTO { }

    public class CouponActivityPageReqByChannelDTO : BasePageRequest
    {
        public CouponActivityChannel ActivityChannel { get; set; }
    }

}
