using Ae.C.MiniApp.Api.Client.Model.Order;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Response.Order
{
    public class GetOrderVerificationCodeInfosClientResponse
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
