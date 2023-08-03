using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Ae.OrderComment.Service.Common.CustomAttribute;

namespace Ae.OrderComment.Service.Core.Enums
{
    public enum CommentTypeEnum
    {
        /// <summary>
        /// 0客户点评
        /// </summary>
        [EnumDescription("客户点评")]
        CustomerComment = 0,
        /// <summary>
        /// 1回复点评
        /// </summary>
        [EnumDescription("回复点评")]
        ReplyComment = 1,
        /// <summary>
        /// 2客户追评
        /// </summary>
        [EnumDescription("客户追评")]
        CustomerAppendComment = 2,
        /// <summary>
        /// 3回复追评
        /// </summary>
        [EnumDescription("回复追评")]
        ReplyAppendComment = 3

    }
}
