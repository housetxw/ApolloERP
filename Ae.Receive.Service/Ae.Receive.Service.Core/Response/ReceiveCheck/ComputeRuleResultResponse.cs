using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Response.ReceiveCheck
{
    /// <summary>
    /// 检查结果
    /// </summary>
    public class ComputeRuleResultResponse
    {
        /// <summary>
        /// 结果值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 结果词
        /// </summary>
        public List<CheckResultWord> ResultWords { get; set; } = new List<CheckResultWord>();
    }
}
