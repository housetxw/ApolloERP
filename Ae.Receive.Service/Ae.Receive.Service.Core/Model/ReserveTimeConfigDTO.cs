using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Model
{
    public class ReserveTimeConfigDTO
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public string StartTime { get; set; }
        /// <summary>
        /// 可预约数量
        /// </summary>
        public int ReserveCount { get; set; }
    }
}
