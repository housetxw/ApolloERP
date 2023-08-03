using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request
{
    /// <summary>
    /// 积分兑换优惠券请求参数
    /// </summary>
    public class IntegralExchangeCouponRequest
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 优惠券领取活动Id
        /// </summary>
        public long ActivityId { get; set; }
    }
}
