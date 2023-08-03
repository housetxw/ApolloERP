using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.Order.Service.Core.Enums
{
    public enum ExceptionOrderEnum
    {
        /// <summary>
        /// 全部
        /// </summary>
        [Description("全部")]
        All = 0,
        /// <summary>
        /// 是
        /// </summary>
        [Description("是")]
        Yes =1,
        /// <summary>
        /// 否
        /// </summary>
        [Description("否")]
        No = 2,
    }
}
