using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Enums
{
    /// <summary>
    /// 点评类型
    /// </summary>
    public enum CommentTypeEnum
    {
        /// <summary>
        /// 客户点评
        /// </summary>
        CoustomerComment = 0,
        /// <summary>
        /// 回复点评
        /// </summary>
        ReplyComment = 1,
        /// <summary>
        /// 客户追评
        /// </summary>
        CoustomerAppendComment = 2,
        /// <summary>
        /// 回复追评
        /// </summary>
        ReplyAppendComment = 3
    }
}
