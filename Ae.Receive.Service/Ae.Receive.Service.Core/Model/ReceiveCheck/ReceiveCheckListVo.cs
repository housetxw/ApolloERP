using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Model.ReceiveCheck
{
    /// <summary>
    /// 检查报告List
    /// </summary>
    public class ReceiveCheckListVo
    {
        /// <summary>
        /// 到店记录Id
        /// </summary>
        public long RecId { get; set; }

        /// <summary>
        /// 里程数
        /// </summary>
        public int Mileage { get; set; }
    }
}
