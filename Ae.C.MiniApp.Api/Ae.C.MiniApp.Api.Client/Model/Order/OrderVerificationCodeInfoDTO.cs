using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Model.Order
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
