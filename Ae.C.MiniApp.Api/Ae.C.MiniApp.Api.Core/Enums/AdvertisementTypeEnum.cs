using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Enums
{
    /// <summary>
    /// AdvertisementTypeEnum
    /// </summary>
    public enum AdvertisementTypeEnum
    {
        /// <summary>
        /// 头部
        /// </summary>
        [Description("头部")] Top = 0,

        /// <summary>
        /// 底部
        /// </summary>
        [Description("底部")] Bottom = 1
    }
}
