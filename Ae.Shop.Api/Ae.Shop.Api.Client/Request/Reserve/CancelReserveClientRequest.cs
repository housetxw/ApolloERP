using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Client.Request.Reserve
{
    public class CancelReserveClientRequest
    {
        /// <summary>
        /// 预约ID
        /// </summary>
        public long ReserveId { get; set; }

        /// <summary>
        /// 取消原因
        /// </summary>
        public string CancelReason { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public string UpdateBy { get; set; }
    }
}
