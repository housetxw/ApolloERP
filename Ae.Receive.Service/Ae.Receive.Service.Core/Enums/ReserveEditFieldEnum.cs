using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.Receive.Service.Core.Enums
{
    /// <summary>
    /// 预约修改字段枚举
    /// </summary>
    public enum ReserveEditFieldEnum
    {
        /// <summary>
        /// 取消原因
        /// </summary>
        [Description("取消原因")] CancelReason = 0,

        /// <summary>
        /// 预约类型
        /// </summary>
        [Description("预约类型")] ReserveType = 1,

        /// <summary>
        /// 预约时间
        /// </summary>
        [Description("预约时间")] ReserveTime = 2,

        /// <summary>
        /// 预约技师
        /// </summary>
        [Description("预约技师")] TechName = 3,

        /// <summary>
        /// 预约备注
        /// </summary>
        [Description("预约备注")] Remark = 4,

        /// <summary>
        /// 车牌
        /// </summary>
        [Description("车牌")] CarPlate = 5,

        /// <summary>
        /// 车型
        /// </summary>
        [Description("车型")] CarType = 6,

        /// <summary>
        /// 客户姓名
        /// </summary>
        [Description("客户姓名")] UserName = 7
    }
}
