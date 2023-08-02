using Ae.B.Product.Api.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.B.Product.Api.Core.Request.Coupon
{
    /// <summary>
    /// 编辑活动
    /// </summary>
    public class EditCouponActivityRequest
    {
        /// <summary>
        /// 活动名称
        /// </summary>
        [Range(1, Int32.MaxValue, ErrorMessage = "活动Id必须大于0")]
        public long CouponActivityId { get; set; }

        /// <summary>
        /// 活动名称
        /// </summary>
        [Required(ErrorMessage = "活动名称不能为空")]
        public string Name { get; set; }

        /// <summary>
        /// 兑换码
        /// </summary>
        public string Code { get; set; } = string.Empty;

        /// <summary>
        /// 优惠券Id
        /// </summary>
        [Range(1, Int32.MaxValue, ErrorMessage = "请选择优惠券")]
        public long CouponId { get; set; }

        /// <summary>
        /// 渠道（0未设置 1小程序 2Android-App 3iOS-App 4S网站 5官网）
        /// </summary>
        public CouponActivityChannel Channel { get; set; }

        /// <summary>
        /// 是否仅新用户可领取
        /// </summary>
        public bool IsNewUserOnly { get; set; }

        /// <summary>
        /// 单用户最大领取数量（0为未设置）
        /// </summary>
        public int MaxNumPerUser { get; set; }

        /// <summary>
        /// 是否是积分兑换活动（默认0否）
        /// </summary>
        public bool IsIntegralExchange { get; set; }

        /// <summary>
        /// 需要积分类型（0未设置 1鸡蛋 2鹅蛋）
        /// </summary>
        public CouponActivityIntegralType NeedIntegralType { get; set; }

        /// <summary>
        /// 需要积分数量
        /// </summary>
        public int NeedIntegralNum { get; set; }

        /// <summary>
        /// 发放总数
        /// </summary>
        public int TotalNum { get; set; }

        /// <summary>
        /// 单次发放数量
        /// </summary>
        public int Num { get; set; }

        /// <summary>
        /// 访问地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 发放方式（0未设置 1系统发放-主动促发  2系统发放-被动响应 3人工发放）
        /// </summary>
        public sbyte GrantPattern { get; set; }

        /// <summary>
        /// 父级活动id
        /// </summary>
        public int ParentId { get; set; }
    }
}
