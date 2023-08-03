using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Response
{
    /// <summary>
    /// 商户处理后同步返回给微信参数
    /// </summary>
    public class WxPayResultNotifyResponse
    {
        /// <summary>
        /// 返回状态码 示例值：SUCCESS
        /// SUCCESS/FAIL
        /// SUCCESS表示商户接收通知成功并校验成功
        /// </summary>
        public string return_code { get; set; }
        /// <summary>
        /// 返回信息 示例值：OK
        /// 返回信息，如非空，为错误原因：
        /// 签名失败
        /// 参数格式校验错误
        /// </summary>
        public string return_msg { get; set; }
    }
}
