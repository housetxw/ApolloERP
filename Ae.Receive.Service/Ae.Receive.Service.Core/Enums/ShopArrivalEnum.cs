using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.Receive.Service.Core.Enums
{
    public enum ShopArrivalStatus
    {
        /// <summary>
        /// 等待中
        /// </summary>
        [Description("等待中")]
        Waiting = 0,

        /// <summary>
        /// 施工中
        /// </summary>
        [Description("施工中")]
        UnderConstruction = 1,

        /// <summary>
        /// 已完结
        /// </summary>
        [Description("已完结")]
        Finished = 2,

        /// <summary>
        /// 未消费离店
        /// </summary>
        [Description("未消费离店")]
        NoPendingLeftShop = 3,
    }

}
