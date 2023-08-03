using Ae.C.MiniApp.Api.Core.Model.Coupon;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Request.Coupon
{
    public class CouponDetailByActIdClientRequest
    {
        /// <summary>
        /// 券活动Id
        /// </summary>
        public long CouponActivityId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 渠道信息"
        /// </summary>
        public CouponActivityChannel ActivityChannel { get; set; }
    }
}
