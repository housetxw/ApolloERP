using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.Receive.Service.Core.Enums
{
    /// <summary>
    /// 
    /// </summary>
    public  enum ReserveTypeEnum
    {
        /// <summary>
        /// 保养服务
        /// </summary>
        [Description("保养服务")]
        BaoYang = 1,
        /// <summary>
        /// 轮胎安装
        /// </summary>
        [Description("轮胎安装")]
        Tire = 2,
        /// <summary>
        /// 美容洗车
        /// </summary>
        [Description("美容洗车")]
        Wash = 3,
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
