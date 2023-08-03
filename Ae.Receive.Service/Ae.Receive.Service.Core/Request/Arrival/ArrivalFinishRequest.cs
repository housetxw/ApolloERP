using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Request.Arrival
{
    public class ArrivalFinishRequest
    {
        /// <summary>
        /// 到店记录
        /// </summary>
        public string ArrivalId { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string UserName { get; set; }
    }
}
