using Ae.ConsumerOrder.Service.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ConsumerOrder.Service.Client.Model.Coupon
{
    public class UserCouponPageResByUserIdDTO
    {
        /// <summary>
        /// 自增主键
        /// </summary>
        public long UserCouponId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 活动Id
        /// </summary>
        public long CouponActivityId { get; set; }

        /// <summary>
        /// 规则Id
        /// </summary>
        public long CouponRuleId { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImageUrl { get; set; } = string.Empty;

        /// <summary>
        /// 面值
        /// </summary>
        public decimal Value { get; set; }

        /// <summary>
        /// 优惠券规则名称（满减券¥20满0元可用）
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 优惠券显示名称（电池优惠券）
        /// </summary>
        public string DisplayName { get; set; } = string.Empty;

        /// <summary>
        /// 使用规则描述
        /// </summary>
        public string UseRuleDesc { get; set; } = string.Empty;

        /// <summary>
        /// 类型（0未设置 1满减券 2实付券）
        /// </summary>
        public CouponType Type { get; set; }

        /// <summary>
        /// 使用状态（0未使用 1已使用 2已过期）
        /// </summary>
        public UserCouponStatus Status { get; set; }

        /// <summary>
        /// ！！！仅当UserCouponStatus(Status)为0时，此字段才有用！！！
        /// </summary>
        public bool OrderEnabled { get; set; }

        /// <summary>
        /// 开始有效时间
        /// </summary>

        public DateTime StartValidTime { get; set; } = new DateTime(1900, 1, 1);

        /// <summary>
        /// 截止有效时间
        /// </summary>
        public DateTime EndValidTime { get; set; } = new DateTime(1900, 1, 1);
    }
}
