using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.OrderComment.Service.Core.Enums
{
    /// <summary>
    /// 回复类型
    /// </summary>
    public enum ReplyTypeEnum
    {
        /// <summary>
        /// 1 回复点评
        /// </summary>
        [Description("回复点评")]
        ReplyComment=1,

        /// <summary>
        /// 2客户追评
        /// </summary>
        [Description("客户追评")]
        CustomerAppendComment = 2,
        /// <summary>
        /// 3回复追评
        /// </summary>
        [Description("回复追评")]
        ReplyAppendComment = 3

    }
}
