using Ae.B.Product.Api.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Model.Coupon
{
    public class GiftCouponRuleDetailDto
    {
        /// <summary>
        /// 规则id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 规则名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 适用渠道
        /// </summary>
        public ChannelEnum Channel { get; set; }

        /// <summary>
        /// 单用户最大享受次数
        /// </summary>
        public int MaxNumPerUser { get; set; }

        /// <summary>
        /// 阈值
        /// </summary>
        public decimal Threshold { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 是否生效
        /// </summary>
        public sbyte Actived { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 优惠券活动
        /// </summary>
        public List<CouponActDto> CouponActList { get; set; }

        /// <summary>
        /// 产品
        /// </summary>
        public List<CouponProductDto> Products { get; set; }
    }

    /// <summary>
    /// CouponActVo
    /// </summary>
    public class CouponActDto
    {
        /// <summary>
        /// 活动id
        /// </summary>
        public long ActId { get; set; }

        /// <summary>
        /// 活动名称
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// CouponProductVo
    /// </summary>
    public class CouponProductDto
    {
        /// <summary>
        /// 产品pid
        /// </summary>
        public string Pid { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string Name { get; set; }
    }
}
