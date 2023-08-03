using Ae.C.MiniApp.Api.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Response
{
    public class GetOrderApplicableCouponListResponse : GetUserCouponListResponse
    {
        /// <summary>
        /// 当前是否可用
        /// </summary>
        public bool IsAvailable { get; set; }
    }
}
