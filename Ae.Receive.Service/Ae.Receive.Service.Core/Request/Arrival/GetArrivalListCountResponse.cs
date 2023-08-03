using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Request.Arrival
{
    public class GetArrivalListCountResponse
    {
        /// <summary>
        /// 等待中数量
        /// </summary>
        public int WaitingNum { get; set; }

        /// <summary>
        /// 派工中数量
        /// </summary>
        public int DispatchingNum { get; set; }

        /// <summary>
        /// 已完结数量
        /// </summary>
        public int FinishNum { get; set; }

        /// <summary>
        /// 离开未消费离店
        /// </summary>
        public int LeaveNum { get; set; }
    }
}
