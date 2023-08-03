using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Request.Reserve
{
    /// <summary>
    /// 预约一店一人一车校验
    /// </summary>
    public class CheckReserveTimeRequest : BaseUserRequest
    {
        /// <summary>
        /// 预约时间
        /// </summary>
        public DateTime ReserveTime { get; set; }
    }
}
