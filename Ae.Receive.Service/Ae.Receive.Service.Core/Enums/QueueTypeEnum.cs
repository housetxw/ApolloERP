using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.Receive.Service.Core.Enums
{
    public enum QueueTypeEnum
    {
        /// <summary>
        /// 预约排队
        /// </summary>
        [Description("预约排队")]
        ReserverQueue=1,
        /// <summary>
        /// 到店排队
        /// </summary>
        [Description("到店排队")]
        ArrivalQueue =2,
    }
}
