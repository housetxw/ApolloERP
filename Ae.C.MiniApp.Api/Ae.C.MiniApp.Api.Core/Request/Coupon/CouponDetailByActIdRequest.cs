using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request.Coupon
{
    /// <summary>
    /// CouponDetailByActIdRequest
    /// </summary>
    public class CouponDetailByActIdRequest
    {
        /// <summary>
        /// 券活动Id
        /// </summary>
        [Required(ErrorMessage = "优惠券活动Id不能为空")]
        public string CouponActivityId { get; set; }
    }
}
