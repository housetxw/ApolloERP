using Ae.C.MiniApp.Api.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Request
{
    public class CancelReserveClientRequest
    {
        /// <summary>
        /// 预约ID
        /// </summary>
        public long ReserveId { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public string UpdateBy { get; set; }
        /// <summary>
        /// 取消原因
        /// </summary>
        public string CancelReason { get; set; }
    }
}
