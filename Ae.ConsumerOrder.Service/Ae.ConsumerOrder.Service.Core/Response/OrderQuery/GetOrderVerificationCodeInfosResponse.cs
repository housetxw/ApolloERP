using Ae.ConsumerOrder.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ConsumerOrder.Service.Core.Response
{
    public class GetOrderVerificationCodeInfosResponse
    {
        /// <summary>
        /// 基本信息集合
        /// </summary>
        public List<OrderVerificationCodeBaseInfoDTO> OrderVerificationCodeBaseInfos { get; set; }
        /// <summary>
        /// 使用规则信息
        /// </summary>
        public VerificationRuleInfoDTO VerificationRuleInfo { get; set; }
    }
}
