using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Enums
{
    /// <summary>
    /// 推广内容转发类型枚举
    /// </summary>
    public enum PromoteContentForwardTypeEnum
    {
        /// <summary>
        /// 未设置
        /// </summary>
        None = 0,
        /// <summary>
        /// 微信朋友
        /// </summary>
        WxFriend = 1,
        /// <summary>
        /// 朋友圈
        /// </summary>
        WxFriendCircle = 2,
        /// <summary>
        /// 复制链接
        /// </summary>
        CopyLink = 3
    }
}
