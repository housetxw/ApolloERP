using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Enums
{
    public enum QuickTakeNumberEnum
    {
        /// <summary>
        /// 预约拿号
        /// </summary>
        Appointment = 0,
        /// <summary>
        /// 预约延迟拿号
        /// </summary>
        AppointmentLater = 1,
        /// <summary>
        /// 排队
        /// </summary>
        Queue = 2,
    }
}
