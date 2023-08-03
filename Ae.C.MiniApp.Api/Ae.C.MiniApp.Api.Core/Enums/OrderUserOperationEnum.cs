using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Enums
{
    /// <summary>
    /// 订单用户可执行操作枚举
    /// </summary>
    public enum OrderUserOperationEnum
    {
        [Description("支付订单")]
        PayOrder = 0,

        [Description("取消订单")]
        CancelOrder = 1,

        [Description("再次购买")]
        BuyAgain = 2,

        [Description("确认收货")]
        ConfirmReceive = 3,

        [Description("申请预约")]
        ApplyReserve = 4,

        [Description("找人代办")]
        FindProxy = 5,

        [Description("评价订单")]
        CommentOrder = 6,

        [Description("追加点评")]
        AppendComment = 7,

        [Description("安装")]
        Install = 8,

        [Description("查看预约")]
        CheckReserve = 10,

        [Description("重新预约")]
        AgainReserve = 11
    }
}
