using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Response
{
    /// <summary>
    /// 获取产品详情页可用优惠券列表返回参数
    /// </summary>
    public class GetProductDetailCouponListResponse
    {
        /// <summary>
        /// 优惠券领取活动Id
        /// </summary>
        public long ActivityId { get; set; }
        /// <summary>
        /// 优惠券使用规则Id
        /// </summary>
        public long RuleId { get; set; }
        /// <summary>
        /// 优惠券显示名称
        /// </summary>
        public string DisplayName { get; set; } = string.Empty;
    }
}
