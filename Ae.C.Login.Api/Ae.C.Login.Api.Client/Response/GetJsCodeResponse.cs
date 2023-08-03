
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.Login.Api.Client.Request
{
    /// <summary>
    /// 获取微信开放openid返回
    /// </summary>
    public class GetJsCodeResponse
    {
        /// <summary>
        /// 用户唯一标识
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 会话密钥
        /// </summary>
        public string session_key { get; set; }
        /// <summary>
        /// 用户在开放平台的唯一标识符，在满足 UnionID 下发条件的情况下会返回，详见 UnionID 机制说明。
        /// </summary>
        public string unionid { get; set; }
        /// <summary>
        /// 错误码 0成功，-1系统繁忙，此时请开发者稍候再试，40029code无效，45011频率限制，每个用户每分钟100次
        /// </summary>
        public int errcode { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string errmsg { get; set; }

    }
}
