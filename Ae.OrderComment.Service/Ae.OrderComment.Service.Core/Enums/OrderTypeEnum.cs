using Ae.OrderComment.Service.Common.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.OrderComment.Service.Core.Enums
{
    /// <summary>
    /// 订单类型枚举
    /// </summary>
    public enum OrderTypeEnum
    {
        /// <summary>
        /// 未设置
        /// </summary>
        [EnumDescription("未设置")]
        None = 0,
        /// <summary>
        /// /轮胎
        /// </summary>
        [EnumDescription("轮胎")]
        Tire = 1,
        /// <summary>
        /// 保养
        /// </summary>
        [EnumDescription("保养")]
        BaoYang = 2,
        /// <summary>
        /// 美容
        /// </summary>
        [EnumDescription("美容")]
        Beauty = 3,
    }

}
