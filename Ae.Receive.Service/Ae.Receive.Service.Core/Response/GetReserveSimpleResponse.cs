using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Response
{
    public class GetReserveSimpleResponse
    {
        /// <summary>
        /// 预约id
        /// </summary>
        public long ReserveId { get; set; }

        /// <summary>
        /// 预约时间
        /// </summary>
        public DateTime ReserveTime { get; set; }
    }
}
