using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Request.Coupon
{
    /// <summary>
    /// GiftCouponRulePageListRequest
    /// </summary>
    public class GiftCouponRulePageListRequest : BasePageRequest
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否生效
        /// </summary>
        public sbyte? Actived { get; set; }
    }
}
