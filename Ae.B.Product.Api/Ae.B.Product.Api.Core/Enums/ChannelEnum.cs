using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.B.Product.Api.Core.Enums
{

    /// <summary>
    /// ChannelEnum
    /// </summary>
    public enum ChannelEnum
    {
        /// <summary>
        /// 未设置
        /// </summary>
        [Description("未设置")] NoSet = 0,

        /// <summary>
        /// 线下
        /// </summary>
        [Description("线下渠道")] OffLine = 1,

        /// <summary>
        /// 线上
        /// </summary>
        [Description("线上渠道")] OnLine = 2
    }
}
