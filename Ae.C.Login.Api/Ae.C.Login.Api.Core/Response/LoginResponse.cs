using Ae.C.Login.Api.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.Login.Api.Core.Response
{
    /// <summary>
    /// 登录返回
    /// </summary>
    public class LoginResponse
    {
        /// <summary>
        /// 用户信息
        /// </summary>
        public UserInfoVO UserInfo { get; set; }
        /// <summary>
        /// 登录token信息
        /// </summary>
        public TokenInfoVO TokenInfo { get; set; }

    }
}
