using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Request.Arrival
{
    public class GetShopArrivalForStaticRequest
    {
        /// <summary>
        /// UserId
        /// </summary>
        public List<string> UserId { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public string StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndTime { get; set; }
    }
}
