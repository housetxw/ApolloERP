using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Model.Service
{
    /// <summary>
    /// 消费历史
    /// </summary>
    public class ConsumptionHistoryVo
    {
        /// <summary>
        /// 到店的总次数
        /// </summary>
        public int ArriveShopSumCount { get; set; } = 0;

        /// <summary>
        /// 消费总计
        /// </summary>
        public string TotalConsumption { get; set; } = string.Empty;

        /// <summary>
        /// 最后到店时间
        /// </summary>
        public string LastArriveShopTime { get; set; } = string.Empty;

        /// <summary>
        /// 最后的公里数
        /// </summary>
        public string LastKilometres { get; set; } = string.Empty;
    }
}
