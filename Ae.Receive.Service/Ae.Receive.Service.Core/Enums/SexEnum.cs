using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.Receive.Service.Core.Enums
{
    public enum SexEnum
    {
        /// <summary>
        /// 保密
        /// </summary>
        [Description("保密")]
        Secrecy = 0,
        /// <summary>
        /// 男
        /// </summary>
        [Description("男")]
        Man = 1,
        /// <summary>
        /// 女
        /// </summary>
        [Description("女")]
        Woman = 2,
    }
}
