using Ae.Receive.Service.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Request
{
    public class UpdateReserveStatusRequest
    {
        /// <summary>
        /// 预约ID
        /// </summary>
        public long ReserveId { get; set; }
        /// <summary>
        /// 预约状态
        /// </summary>
        public ReserveStatusEnum ReserveStatus { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public string UpdateBy { get; set; }
    }
}
