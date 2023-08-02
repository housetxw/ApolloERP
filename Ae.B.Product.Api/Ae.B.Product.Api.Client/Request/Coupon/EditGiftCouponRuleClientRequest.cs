using Ae.B.Product.Api.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Request.Coupon
{
    public class EditGiftCouponRuleClientRequest
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>

        public string Name { get; set; }

        /// <summary>
        /// 渠道
        /// </summary>
        public ChannelEnum? Channel { get; set; }

        /// <summary>
        /// 单用户最大享用次数
        /// </summary>
        public int? MaxNumPerUser { get; set; }

        /// <summary>
        /// 阈值
        /// </summary>
        public decimal? Threshold { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 是否激活
        /// </summary>
        public sbyte? Actived { get; set; }

        /// <summary>
        /// 提交人
        /// </summary>
        public string SubmitBy { get; set; }

        /// <summary>
        /// 优惠券活动id
        /// </summary>
        public List<long> CouponActivityId { get; set; }

        /// <summary>
        /// 产品pid
        /// </summary>
        public List<string> PidList { get; set; }
    }
}
