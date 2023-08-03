using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.Order.Service.Core.Enums
{
    public enum OrderTypeEnum
    {
        /// <summary>
        /// 未设置
        /// </summary>
        [Description("未设置")]
        None = 0,
        [Description("轮胎")]
        Tire =1,
        [Description("保养")]
        BaoYang =2,
        [Description("美容")]
        Beauty =3,
        [Description("钣金维修")]
        Spray =4,
        [Description("汽车改装")]
        CarRefitting=5,
        [Description("其他")]
        Other=6,
    }
}
