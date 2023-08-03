using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.Login.Api.Core.Model
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class UserInfoVO
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string HeadUrl { get; set; }

        /// <summary>
        /// 用户三方唯一标识
        /// </summary>
        public string OpenId { get; set; }
        /// <summary>
        /// 用户手机
        /// </summary>
        public string MobileNumber { get; set; }
        /// <summary>
        /// 是否锁定
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// 渠道来源
        /// </summary>
        public int Channel { get; set; }
        /// <summary>
        /// 推荐人Id
        /// </summary>
        public string ReferrerUserId { get; set; } = string.Empty;
    }
}
