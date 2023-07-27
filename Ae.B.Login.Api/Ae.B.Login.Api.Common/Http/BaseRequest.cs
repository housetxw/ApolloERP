using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.B.Login.Api.Common.Http
{
    /// <summary>
    /// 请求基础类
    /// </summary>
    public class BaseRequest
    {
        [Description("请求渠道，安卓: a-xxx,IOS: ios-xxx,PC: s-pc, b-pc")]
        public string Channel { get; set; }

        [Description("版本号，例如：1.0")]
        public string ApiVersion { get; set; }

        [Description("设备号")]
        public string DeviceId { get; set; }
    }

    public class BaseEntityPostRequest<T> : BaseRequest
    {
        public T Data { get; set; }
    }
}
