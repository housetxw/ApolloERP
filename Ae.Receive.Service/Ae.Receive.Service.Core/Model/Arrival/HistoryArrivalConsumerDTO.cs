using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Model.Arrival
{
    /// <summary>
    /// 历史到店消费DTO
    /// </summary>
    public class HistoryArrivalConsumerDTO
    {
        /// <summary>
        /// 历史到店总次数
        /// </summary>
        public int ArrivalSumCount { get; set; }

        /// <summary>
        /// 历史到店总金额
        /// </summary>
        public decimal SumPrice { get; set; }

        /// <summary>
        /// 最后到店时间
        /// </summary>
        public string LastArrivalTime { get; set; }
    }
}
