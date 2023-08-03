using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.Login.Api.Core.Model
{
    /// <summary>
    /// token对象
    /// </summary>
    public class TokenInfoVO
    {
        /// <summary>
        /// 刷新地址
        /// </summary>
        public string RefreshUri { get; set; }
        /// <summary>
        /// Token
        /// </summary>
        public string AccessToken { get; set; }
        /// <summary>
        /// 刷新Token
        /// </summary>
        public string RefreshToken { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime Expires { get; set; }
    }
}
