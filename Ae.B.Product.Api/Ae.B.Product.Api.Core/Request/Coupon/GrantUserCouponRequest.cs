using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.B.Product.Api.Core.Request.Coupon
{
    /// <summary>
    /// 
    /// </summary>
    public class GrantUserCouponRequest
    {
        /// <summary>
        /// 活动Id
        /// </summary>
        [Range(1, Int64.MaxValue, ErrorMessage = "活动Id必须大于0")]
        public long ActivityId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        [Required(ErrorMessage = "用户Id不能为空")]
        [MinLength(1, ErrorMessage = "活动Id不能为空")]
        public List<string> UserIdList { get; set; }
    }
}
