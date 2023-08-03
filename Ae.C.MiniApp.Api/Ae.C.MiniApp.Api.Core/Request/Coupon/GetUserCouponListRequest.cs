using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request
{
    /// <summary>
    /// 获取用户优惠券列表请求参数
    /// </summary>
    public class GetUserCouponListRequest : BasePageRequest
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }
    }
}
