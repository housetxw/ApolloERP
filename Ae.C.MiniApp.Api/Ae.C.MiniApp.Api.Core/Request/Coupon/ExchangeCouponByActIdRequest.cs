using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request.Coupon
{
    /// <summary>
    /// ExchangeCouponByActIdRequest
    /// </summary>
    public class ExchangeCouponByActIdRequest
    {
        /// <summary>
        /// 券活动Id
        /// </summary>
        [Required(ErrorMessage = "请输入优惠券活动Id")]
        public string CouponActivityId { get; set; }
    }
}
