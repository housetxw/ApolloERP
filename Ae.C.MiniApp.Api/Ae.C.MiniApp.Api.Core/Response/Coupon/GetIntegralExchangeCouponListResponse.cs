using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Response
{
    /// <summary>
    /// 获取产品详情页可用优惠券列表返回参数
    /// </summary>
    public class GetIntegralExchangeCouponListResponse
    {
        /// <summary>
        /// 优惠券领取活动Id
        /// </summary>
        public long ActivityId { get; set; }
        /// <summary>
        /// 需要积分类型（0未设置 1鸡蛋 2鹅蛋）
        /// </summary>
        public sbyte NeedIntegralType { get; set; }
        /// <summary>
        /// 需要积分数量
        /// </summary>
        public int NeedIntegralNum { get; set; }

        /// <summary>
        /// 优惠券使用规则Id
        /// </summary>
        public long RuleId { get; set; }
        /// <summary>
        /// 优惠券显示名称
        /// </summary>
        public string DisplayName { get; set; } = string.Empty;
        /// <summary>
        /// 面值
        /// </summary>
        public decimal Value { get; set; }
        /// <summary>
        /// 使用级别阈值
        /// </summary>
        public decimal Threshold { get; set; }
        /// <summary>
        /// 使用规则描述
        /// </summary>
        public string UseRuleDesc { get; set; } = string.Empty;
        /// <summary>
        /// 开始有效时间
        /// </summary>
        public DateTime StartValidTime { get; set; }
        /// <summary>
        /// 截止有效时间
        /// </summary>
        public DateTime EndValidTime { get; set; }
    }
}
