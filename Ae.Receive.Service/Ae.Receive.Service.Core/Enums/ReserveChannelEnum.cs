using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.Receive.Service.Core.Enums
{
    public enum ReserveChannelEnum
    {
        /// <summary>
        /// c小程序预约
        /// </summary>
        [Description("线上预约")]
        WechatApplet = 1,
        /// <summary>
        /// 门店
        /// </summary>
        [Description("门店预约")]
        Shop = 2,
    }
}
