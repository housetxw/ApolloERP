using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.ConsumerOrder.Service.Core.Enums
{
    public enum OrderServiceTypeEnum
    {
        /// <summary>
        /// 未设置
        /// </summary>
        [Description("上门服务")]
        DoorToDoor = 0,
        /// <summary>
        /// 轮胎
        /// </summary>
        [Description("到店服务")]
        ArrivalToShop,
    }
}
