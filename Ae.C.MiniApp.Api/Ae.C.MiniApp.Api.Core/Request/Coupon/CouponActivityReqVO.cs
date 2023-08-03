using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Ae.C.MiniApp.Api.Core.Model.Coupon;

namespace Ae.C.MiniApp.Api.Core.Request.Coupon
{
    public class CouponActivityReqVO { }

    public class CouponActivityPageReqByChannelVO : BasePageRequest { }

    public class CouponActivityPageReqByChannelVOForDTO : BasePageRequest
    {
        [Required(ErrorMessage = "请输入渠道信息")]
        [Range(0, (int)CouponActivityChannel.WebSite, ErrorMessage = "渠道信息必须大于0")]
        public CouponActivityChannel ActivityChannel { get; set; }
    }

}
