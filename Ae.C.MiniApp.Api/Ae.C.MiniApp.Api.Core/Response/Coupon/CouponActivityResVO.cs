using System;
using System.Collections.Generic;
using System.Text;
using Ae.C.MiniApp.Api.Core.Model.Coupon;

namespace Ae.C.MiniApp.Api.Core.Response.Coupon
{
    public class CouponActivityResVO { }

    public class CouponActivityPageResVO
    {
        /// <summary>
        /// 活动Id
        /// </summary>
        public long CouponActivityId { get; set; }

        /// <summary>
        /// 活动名称
        /// </summary>
        public string CouponActivityName { get; set; }

        /// <summary>
        /// 规则Id
        /// </summary>
        public long CouponRuleId { get; set; }

        public string Category { get; set; }

        /// <summary>
        /// 渠道（0未设置 1小程序 2Android-App 3iOS-App 4S网站 5官网）
        /// </summary>
        public CouponActivityChannel Channel { get; set; }

        /// <summary>
        /// 需要积分数量
        /// </summary>
        public int NeedIntegralNum { get; set; }

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
        public string CouponRuleName { get; set; } = string.Empty;

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
        /// 开始有效时间
        /// </summary>
        public DateTime StartTime { get; set; } = new DateTime(1900, 1, 1);

        /// <summary>
        /// 截止有效时间
        /// </summary>
        public DateTime EndTime { get; set; } = new DateTime(1900, 1, 1);

        public bool IsNewUserOnly { get; set; }
    }

}
