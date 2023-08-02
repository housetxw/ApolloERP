using Ae.B.Product.Api.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Response
{
    /// <summary>
    /// 优惠券活动配置
    /// </summary>
    public class GetCouponActivityListForShopResponse
    {
        public long Id { get; set; }

        /// <summary>
        /// 活动Id
        /// </summary>
        public long CouponActivityId { get; set; }

        /// <summary>
        /// 活动名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 优惠券Id
        /// </summary>
        public long CouponId { get; set; }

        /// <summary>
        /// 优惠券名称
        /// </summary>
        public string CouponName { get; set; }

        /// <summary>
        /// 发放总数
        /// </summary>
        public int TotalNum { get; set; }

        /// <summary>
        /// 领取总数
        /// </summary>
        public int ReceiveNum { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 状态（0未发布 1可领取 2暂停领取 3已作废）
        /// </summary>
        public CouponActivityStatue Status { get; set; }

        /// <summary>
        /// 兑换码
        /// </summary>
        public string Code { get; set; }

        public int Type { get; set; }

        public Decimal Value { get; set; }

        public Decimal Threshold { get; set; }

        public String UseRuleDesc { get; set; }

        public string ShopId { get; set; }

    }
}
