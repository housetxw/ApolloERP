using Ae.B.Product.Api.Core.Request.Coupon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.B.Product.Api.Client.Request.Coupon
{
    public class UserCouponPageByUserIdClientRequest
    {
        public string UserId { get; set; }

        /// <summary>
        /// 订单可用优惠券UserCouponId集合
        /// ！！！字段用途说明！！！
        /// 仅当UserCouponStatus(Status)为0时，此字段才有用，且目前只能用于订单确认页的入参中
        /// </summary>
        public List<long> UserCouponId { get; set; } = new List<long>();


        public CouponListEntranceType EntranceType { get; set; }

        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; } = 10;
    }
}
