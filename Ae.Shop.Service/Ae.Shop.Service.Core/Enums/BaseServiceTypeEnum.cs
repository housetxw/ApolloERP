using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.Shop.Service.Core.Enums
{
    public enum BaseServiceTypeEnum
    {
        /// <summary>
        /// 美容服务,2轮胎服务TireService,3保养服务MaintenanceService
        /// </summary>
        [Description("洗车服务")]
        CarBeauty = 1,
        /// <summary>
        /// 轮胎服务,3保养服务MaintenanceService
        /// </summary>
        [Description("轮胎服务")]
        TireService = 2,
        /// <summary>
        /// 保养服务MaintenanceService
        /// </summary>
        [Description("保养服务")]
        MaintenanceService = 3,

    }
}
