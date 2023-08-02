using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Order.Api.Core.Model.OrderDetail
{
    public class OrderCouponInformationDto
    {
        /// <summary>
        /// 优惠券规则名称（满减券¥20满0元可用）
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 优惠券显示名称（电池优惠券）
        /// </summary>
        public string DisplayName { get; set; } = string.Empty;

        /// <summary>
        /// 面值
        /// </summary>
        public decimal Value { get; set; }

       

        /// <summary>
        /// 优惠卷类型描述
        /// </summary>
        public string CouponTypeText
        {
            get;set;
        }

  

        public string GrantMethodText
        {
            get; set;
        }

        /// <summary>
        /// 活动名称
        /// </summary>
        public string ActivityName { get; set; }

    }
}
