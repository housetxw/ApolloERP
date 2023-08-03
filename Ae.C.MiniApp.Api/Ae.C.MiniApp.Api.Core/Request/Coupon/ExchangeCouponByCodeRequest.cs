using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request
{
    /// <summary>
    /// 优惠码兑换优惠券请求参数
    /// </summary>
    public class ExchangeCouponByCodeRequest
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 兑换码
        /// </summary>
        public string Code { get; set; }
    }
}
