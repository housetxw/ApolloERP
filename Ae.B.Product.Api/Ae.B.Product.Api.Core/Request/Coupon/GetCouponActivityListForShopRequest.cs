using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Request.Coupon
{
    public class GetCouponActivityListForShopRequest : BasePageRequest
    {
        public long Id { get; set; }
        /// <summary>
        /// 活动名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 优惠卷名称
        /// </summary>
        public string CouponName { get; set; }

        /// <summary>
        /// 活动状态 （-1查所有  0未发布  1可领取  2暂停领取  3已作废）
        /// </summary>
        public int Status { get; set; } = -1;


        /// <summary>
        /// 兑换码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 门店Id
        /// </summary>
        public long ShopId { get; set; }

        /// <summary>
        /// 门店类型
        /// </summary>
        public int ShopType { get; set; }
    }
}
