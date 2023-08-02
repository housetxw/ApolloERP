using Ae.B.Product.Api.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.B.Product.Api.Core.Request.Coupon
{
    /// <summary>
    /// AddGiftCouponRuleRequest
    /// </summary>
    public class AddGiftCouponRuleRequest
    {
        /// <summary>
        /// 名称
        /// </summary>

        [Required(ErrorMessage = "名称不能为空")]
        public string Name { get; set; }

        /// <summary>
        /// 渠道
        /// </summary>
        public ChannelEnum Channel { get; set; }

        /// <summary>
        /// 单用户最大享用次数
        /// </summary>
        public int MaxNumPerUser { get; set; }

        /// <summary>
        /// 阈值
        /// </summary>
        public decimal Threshold { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public string StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndTime { get; set; }

        /// <summary>
        /// 是否激活
        /// </summary>
        public sbyte Actived { get; set; }

        /// <summary>
        /// 优惠券活动id
        /// </summary>
        [Required(ErrorMessage = "请选择优惠券活动")]
        [MinLength(1, ErrorMessage = "请选择优惠券活动")]
        public List<long> CouponActivityId { get; set; }

        /// <summary>
        /// 产品pid
        /// </summary>
        [Required(ErrorMessage = "请选择产品")]
        [MinLength(1, ErrorMessage = "请选择产品")]
        public List<string> PidList { get; set; }
    }
}
