using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.Shop.Api.Core.Enums
{
    public enum CommentTypeEnum
    {
        /// <summary>
        /// 0客户点评
        /// </summary>
        [Description("客户点评")]
        CustomerComment = 0,
        /// <summary>
        /// 1回复点评
        /// </summary>
        [Description("回复点评")]
        ReplyComment = 1,
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
