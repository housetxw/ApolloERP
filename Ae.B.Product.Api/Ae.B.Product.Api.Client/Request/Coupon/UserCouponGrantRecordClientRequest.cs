using Ae.B.Product.Api.Core.Model.Coupon;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Request.Coupon
{
    public class UserCouponGrantRecordClientRequest
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
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 领取起始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 领取结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 优惠券使用状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 用户优惠券：发放方式
        /// </summary>
        public UserCouponGrantMethod GrantMethod { get; set; }

        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; } = 10;

        public long ShopId { get; set; }
    }
}
