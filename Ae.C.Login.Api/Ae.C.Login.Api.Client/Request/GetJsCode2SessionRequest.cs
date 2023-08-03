using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.C.Login.Api.Client.Request
{
    public class GetJsCode2SessionRequest
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
        public string Js_Code { get; set; }
        /// <summary>
        /// 授权类型，此处只需填写 authorization_code
        /// </summary>
        public string Grant_Type { get; set; }
    }
}
