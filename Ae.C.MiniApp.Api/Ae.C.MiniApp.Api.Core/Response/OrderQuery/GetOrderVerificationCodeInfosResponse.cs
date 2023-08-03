using Ae.C.MiniApp.Api.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Response
{
    public class GetOrderVerificationCodeInfosResponse
    {
        /// <summary>
        /// 基本信息集合
        /// </summary>
        public List<OrderVerificationCodeBaseInfoVO> OrderVerificationCodeBaseInfos { get; set; }
        /// <summary>
        /// 使用规则信息
        /// </summary>
        public VerificationRuleInfoVO VerificationRuleInfo { get; set; }
    }
}
