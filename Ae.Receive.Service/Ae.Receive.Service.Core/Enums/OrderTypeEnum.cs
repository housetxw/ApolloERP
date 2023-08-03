﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.Receive.Service.Core.Enums
{
    public enum OrderTypeEnum
    {

        /// <summary>
        /// 未设置
        /// </summary>
        [Description("未设置")]
        None = 0,
        /// <summary>
        /// 轮胎
        /// </summary>
        [Description("轮胎安装")]
        Tire = 1,
        /// <summary>
        /// 保养
        /// </summary>
        [Description("保养服务")]
        Maintenance = 2,
        /// <summary>
        /// 美容
        /// </summary>
        [Description("美容洗车")]
        Beauty = 3,
        /// <summary>
        /// 钣金维修
        /// </summary>
        [Description("钣金维修")]
        SheetMetal = 4,
        /// <summary>
        /// 汽车改装
        /// </summary>
        [Description("汽车改装")]
        CarModification = 5,
        /// <summary>
        /// 其他
        /// </summary>
        [Description("其他")]
        Other = 6
    }
}
