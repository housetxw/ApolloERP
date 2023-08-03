using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Model
{
    /// <summary>
    /// 订单核销码信息
    /// </summary>
    public class OrderVerificationCodeInfoVO : OrderVerificationCodeBaseInfoVO
    {
        /// <summary>
        /// 核销规则信息
        /// </summary>
        public VerificationRuleInfoVO VerificationRuleInfo { get; set; }
    }
}
