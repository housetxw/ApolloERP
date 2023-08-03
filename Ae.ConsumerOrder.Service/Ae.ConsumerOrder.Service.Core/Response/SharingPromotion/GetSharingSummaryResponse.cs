using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ConsumerOrder.Service.Core.Response.SharingPromotion
{
    public class GetSharingSummaryResponse
    {
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 分享得用户数量
        /// </summary>
        public int NumberOfSharedUsers { get; set; }

        /// <summary>
        /// 已推送优惠卷得数量
        /// </summary>
        public int NumberOfDiscountNumPushed { get; set; }
    }
}
