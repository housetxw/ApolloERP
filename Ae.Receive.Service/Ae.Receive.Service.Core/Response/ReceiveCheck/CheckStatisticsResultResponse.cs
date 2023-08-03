using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Response.ReceiveCheck
{
    /// <summary>
    /// 检查统计
    /// </summary>
    public class CheckStatisticsResultResponse
    {
        /// <summary>
        /// 正常项 
        /// </summary>
        public int NormalCount { get; set; }

        /// <summary>
        /// 异常项
        /// </summary>
        public int AbNormalCount { get; set; }

        /// <summary>
        /// 未检查项
        /// </summary>
        public int UncheckCount { get; set; }
    }
}
