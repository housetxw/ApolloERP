using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.Receive.Service.Core.Enums
{
    public enum ArrivedStatusEnum
    {
        /// <summary>
        /// 到店
        /// </summary>
        [Description("到店")]
        ArrivedShop = 1,
        /// <summary>
        /// 逾期
        /// </summary>
        [Description("逾期")]
        Overdue = 2,
        /// <summary>
        /// 到店逾期
        /// </summary>
        [Description("到店逾期")]
        ArrivedShopOverdue = 3,
    }
}
