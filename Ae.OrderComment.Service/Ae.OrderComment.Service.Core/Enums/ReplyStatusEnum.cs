using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.OrderComment.Service.Core.Enums
{
    /// <summary>
    /// 回复状态枚举
    /// </summary>
    public enum  ReplyStatusEnum
    {
        /// <summary>
        /// 0商家未回复
        /// </summary>
        [Description("商家未回复")]
        ShopNotReply=0,
        /// <summary>
        /// 1商家回复
        /// </summary>
        [Description("商家回复")]
        ShopReply = 1
    }
}
