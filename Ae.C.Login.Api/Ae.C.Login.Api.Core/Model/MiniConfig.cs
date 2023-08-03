using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.Login.Api.Core.Model
{
    /// <summary>
    /// 小程序配置
    /// </summary>
    public class MiniConfig
    {
        /// <summary>
        /// 小程序 appId
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// 小程序 appSecret
        /// </summary>
        public string Secret { get; set; }
        /// <summary>
        /// 登录时获取的 code
        /// </summary>
        public string JsCode { get; set; }
        /// <summary>
        /// 授权类型，此处只需填写 authorization_code
        /// </summary>
        public string GrantType { get; set; }
    }
}
