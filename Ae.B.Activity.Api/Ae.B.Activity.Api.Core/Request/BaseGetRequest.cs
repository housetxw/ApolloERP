using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.B.Activity.Api.Core.Request
{
    /// <summary>
    /// Get请求基础类
    /// </summary>
    public class BaseGetRequest
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseGetRequest() { }
        /// <summary>
        /// 请求渠道
        /// </summary>
        [Description("请求渠道，安卓:a-xxx,IOS:ios-xxx,PC:pc")]
        public string Channel { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        [Description("版本号，例如：1.0")]
        public string ApiVersion { get; set; }
        /// <summary>
        /// 设备号
        /// </summary>
        [Description("设备号")]
        public string DeviceId { get; set; }
    }
}
