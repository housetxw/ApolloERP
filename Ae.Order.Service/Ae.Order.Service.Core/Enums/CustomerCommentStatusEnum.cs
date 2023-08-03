using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Order.Service.Core.Enums
{
    /// <summary>
    /// 客户点评的状态
    /// </summary>
    public enum  CustomerCommentStatusEnum
    {
        /// <summary>
        /// 0待客户点评
        /// </summary>
        WaitingComment = 0,
        /// <summary>
        /// 1客户已点评
        /// </summary>
        CustomerComment=1,
        /// <summary>
        ///  2 客户已追评
        /// </summary>
        CustomerAppendComment = 2,
    }
}
