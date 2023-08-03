using Ae.FMS.Service.Common.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.FMS.Service.Core.Enums
{
    /// <summary>
    /// 到账状态枚举
    /// </summary>
    public enum MoneyArriveStatusEnum
    {
      
        /// <summary>
        /// 未到账
        /// </summary>
        [EnumDescription("未到账")]
        NotArrive = 0,
        /// <summary>
        /// 已到账
        /// </summary>
        [EnumDescription("已到账")]
        Arrive = 1

    }
}
