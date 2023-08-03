using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ConsumerOrder.Service.Core.Model
{
    /// <summary>
    /// 核销规则信息
    /// </summary>
    public class VerificationRuleInfoDTO
    {
        /// <summary>
        /// 核销码显示名称，例：核销码
        /// </summary>
        public string DisplayCodeName { get; set; } = string.Empty;
        /// <summary>
        /// 截止有效时间
        /// </summary>
        public DateTime EndValidTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 适用范围，例：仅洗车
        /// </summary>
        public string ApplicableRange { get; set; } = "仅洗车";//TODO: 暂时写死，以后读取配置
        /// <summary>
        /// 所有适用的门店ID集合
        /// </summary>
        public List<long> ShopIds { get; set; } = new List<long>();
        /// <summary>
        /// 适用规则描述
        /// </summary>
        public string RuleDesc { get; set; } = "支持预约，消费高峰期可能需要等位。发票问题请询问平台。不兑现、不找零。具体解释权归平台所有。";//TODO: 暂时写死，以后读取配置
    }
}
