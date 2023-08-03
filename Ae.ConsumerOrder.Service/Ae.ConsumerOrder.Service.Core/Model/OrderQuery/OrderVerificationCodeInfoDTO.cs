using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ConsumerOrder.Service.Core.Model
{
    /// <summary>
    /// 订单核销码信息
    /// </summary>
    public class OrderVerificationCodeInfoDTO : OrderVerificationCodeBaseInfoDTO
    {
        /// <summary>
        /// 核销规则信息
        /// </summary>
        public VerificationRuleInfoDTO VerificationRuleInfo { get; set; }
    }
}
