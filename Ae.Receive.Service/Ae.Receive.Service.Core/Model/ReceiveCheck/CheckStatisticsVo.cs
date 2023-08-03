using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Model.ReceiveCheck
{
    /// <summary>
    /// 检查统计
    /// </summary>
    public class CheckStatisticsVo
    {
        /// <summary>
        /// 检查Id
        /// </summary>
        public long CheckId { get; set; }

        /// <summary>
        /// 0待提交 1已提交
        /// </summary>
        public int Status { get; set; }

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
