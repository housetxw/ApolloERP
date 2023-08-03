using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.Receive.Service.Core.Enums
{
    /// <summary>
    /// 
    /// </summary>
    public enum ReserveStatusForViewEnum
    {
        /// <summary>
        /// 待确认
        /// </summary>
        [Description("待确认")] Unconfirmed = 0,

        /// <summary>
        /// 有效预约
        /// </summary>
        [Description("")] Valid = 1,

        /// <summary>
        /// 已到店
        /// </summary>
        [Description("已到店")] Completed = 2,

        /// <summary>
        /// 已取消
        /// </summary>
        [Description("已取消")] Canceled = 3,

        /// <summary>
        /// 逾期已到店
        /// </summary>
        [Description("逾期已到店")] OverdueAndArrive = 4,

        /// <summary>
        /// 逾期未到店
        /// </summary>
        [Description("逾期未到店")] OverdueNotArrive = 5
    }
}
