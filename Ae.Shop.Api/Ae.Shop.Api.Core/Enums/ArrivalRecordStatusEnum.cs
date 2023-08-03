using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Enums
{
    /// <summary>
    /// 到店记录枚举
    /// </summary>
    public enum ArrivalRecordStatusEnum
    {
        /// <summary>
        /// 所有
        /// </summary>
        All = -1,
        /// <summary>
        /// 等待中
        /// </summary>
        Waiting = 0,
        /// <summary>
        /// 派工中
        /// </summary>
        Dispatching = 1,
        /// <summary>
        /// 已完结
        /// </summary>
        Finish = 2,
        /// <summary>
        /// 离开未消费离店
        /// </summary>
        Leave = 3,
      
    }
}
