using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Order.Service.Core.Enums
{
    public enum StockStatusEnum
    {
        /// <summary>
        /// 未占库
        /// </summary>
        NoStock=0,
        /// <summary>
        /// 占库中
        /// </summary>
        Stocking=1,
        /// <summary>
        /// 已展开
        /// </summary>
        HavedStock=2,
        /// <summary>
        /// 释放中
        /// </summary>
        Releasing=3,

        /// <summary>
        /// 已释放
        /// </summary>
        Released=4,
    }
}
