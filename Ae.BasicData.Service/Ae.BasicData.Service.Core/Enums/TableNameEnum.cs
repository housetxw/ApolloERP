using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.BasicData.Service.Core.Enums
{
    public enum TableNameEnum
    {
        /// <summary>
        /// app_version
        /// </summary>
        [Description("app_version")]
        AppVersion = 0,

        /// <summary>
        /// app_version_gray
        /// </summary>
        [Description("app_version_gray")]
        AppVersionGray = 1,

        /// <summary>
        /// RegionChina
        /// </summary>
        [Description("region_china")]
        RegionChina = 2,

        /// <summary>
        /// ElapsedTime
        /// </summary>
        [Description("ElapsedTime")]
        ElapsedTime = 999
    }
}
