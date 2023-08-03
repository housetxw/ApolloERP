using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Request.Coupon
{
    public class DrawDecapAwardClientRequest
    {
        /// <summary>
        /// 奖励Id
        /// </summary>
        public long AwardId { get; set; }

        /// <summary>
        /// 兑换码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// OpenId
        /// </summary>
        public string OpenId { get; set; }

        /// <summary>
        /// 领取渠道
        /// </summary>
        public string ClientChannel { get; set; }
    }
}
