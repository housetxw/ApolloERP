using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Request.Coupon
{
    /// <summary>
    /// 优惠券配置查询
    /// </summary>
    public class GetCouponRuleListRequest : BasePageRequest
    {
        /// <summary>
        /// 优惠券规则ID
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 优惠券规则名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 优惠券类型  1：满减  2：实付
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 发行分类（0-未设置 1-总部发行 2-地方发行）
        /// </summary>
        public int Category { get; set; }
        /// <summary>
        /// 券使用范围（0未设置 1线上线下通用 2只限线上使用 3只限线下使用）
        /// </summary>
        public int RangeType { get; set; }

        /// <summary>
        /// 门店
        /// </summary>
        public long ShopId { get; set; }
    }
}
