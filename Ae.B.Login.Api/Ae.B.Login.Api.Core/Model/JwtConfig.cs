using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Login.Api.Core.Model
{
    /// <summary>
    /// jwt配置读取
    /// </summary>
    public class JwtConfig
    {
        /// <summary>
        /// 发行人
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// 接收人
        /// </summary>
        public string AccessTokenAudience { get; set; }

        /// <summary>
        /// 访问Token有效期长
        /// </summary>
        public int AccessTokenExpiresMinutes { get; set; }

        /// <summary>
        /// 刷新Token接收人
        /// </summary>
        public string RefreshTokenAudience { get; set; }

        /// <summary>
        /// 刷新Token有效期长
        /// </summary>
        public int RefreshTokenExpiresMinutes { get; set; }

        /// <summary>
        /// 发行人签名
        /// </summary>
        public string IssuerSigningKey { get; set; }
    }
}
