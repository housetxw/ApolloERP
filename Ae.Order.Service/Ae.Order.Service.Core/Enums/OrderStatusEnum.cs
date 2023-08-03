using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.Order.Service.Core.Enums
{
    public enum OrderStatusEnum
    {
        [Description("已提交")]
        Submitted = 10,

        [Description("已确认")]
        Confirmed = 20,

        [Description("已完成")]
        Completed = 30,

        [Description("已取消")]
        Canceled = 40
    }
}
