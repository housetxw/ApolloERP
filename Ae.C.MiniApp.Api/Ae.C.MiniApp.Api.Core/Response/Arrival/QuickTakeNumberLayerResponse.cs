using Ae.C.MiniApp.Api.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Response.Arrival
{
    /// <summary>
    /// 快速排队的弹层返回对象
    /// </summary>
    public class QuickTakeNumberLayerResponse
    {
        /// <summary>
        /// 拿号类型
        /// </summary>
        public QuickTakeNumberEnum QuickTakeNumberType { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
    }
}
