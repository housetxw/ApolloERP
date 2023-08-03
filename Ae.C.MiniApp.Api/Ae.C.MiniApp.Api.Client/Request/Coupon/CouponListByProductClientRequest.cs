using Ae.C.MiniApp.Api.Core.Model.Coupon;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Request.Coupon
{
    public class CouponListByProductClientRequest
    {
        /// <summary>
        /// 产品pid
        /// </summary>
        public string Pid { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 渠道信息
        /// </summary>
        public CouponActivityChannel ActivityChannel { get; set; }
    }
}
