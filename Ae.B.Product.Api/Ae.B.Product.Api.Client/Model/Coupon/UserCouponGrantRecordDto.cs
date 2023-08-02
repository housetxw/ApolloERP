using Ae.B.Product.Api.Core.Model.Coupon;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Model.Coupon
{
    public class UserCouponGrantRecordDto
    {
        /// <summary>
        /// 用户优惠券Id
        /// </summary>
        public long UserCouponId { get; set; }

        /// <summary>
        /// 用户优惠券：发放方式
        /// </summary>
        public UserCouponGrantMethod GrantMethod { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户手机号
        /// </summary>
        public string UserTel { get; set; }

        /// <summary>
        /// 用户手机号 脱敏
        /// </summary>
        public string UserTelDes { get; set; }

        /// <summary>
        /// 活动Id
        /// </summary>
        public long CouponActivityId { get; set; }

        /// <summary>
        /// 活动名称
        /// </summary>
        public string CouponActivityName { get; set; }

        /// <summary>
        /// 规则Id
        /// </summary>
        public long CouponRuleId { get; set; }

        /// <summary>
        /// 优惠券名称
        /// </summary>
        public string CouponRuleName { get; set; }

        /// <summary>
        /// 使用状态（0未使用 1已使用 2已过期）
        /// </summary>
        public UserCouponStatus Status { get; set; }

        /// <summary>
        /// 开始有效时间
        /// </summary>
        public DateTime StartValidTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 截止有效时间
        /// </summary>
        public DateTime EndValidTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}
