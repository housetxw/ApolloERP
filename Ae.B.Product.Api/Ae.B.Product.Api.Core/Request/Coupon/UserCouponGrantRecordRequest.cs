using Ae.B.Product.Api.Core.Model.Coupon;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Request.Coupon
{
    /// <summary>
    /// 
    /// </summary>
    public class UserCouponGrantRecordRequest : BasePageRequest
    {
        /// <summary>
        /// 优惠券规则Id
        /// </summary>
        public long CouponRuleId { get; set; }

        /// <summary>
        /// 优惠券活动Id
        /// </summary>
        public long CouponActivityId { get; set; }

        /// <summary>
        /// 用户手机号
        /// </summary>
        public string UserTel { get; set; }

        /// <summary>
        /// 领取起始时间 yyyy-MM-dd
        /// </summary>
        public string StartTime { get; set; }

        /// <summary>
        /// 领取结束时间 yyyy-MM-dd
        /// </summary>
        public string EndTime { get; set; }

        /// <summary>
        /// 优惠券使用状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 用户优惠券：发放方式
        /// </summary>
        public UserCouponGrantMethod GrantMethod { get; set; } = UserCouponGrantMethod.NotSet;

        public long ShopId { get; set; }
    }
}
