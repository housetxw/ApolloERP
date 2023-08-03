using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Ae.FMS.Service.Core.Enums
{
    /// <summary>
    /// 对账单类型
    /// </summary>
    public enum AccountTypeEnum
    {
        /// <summary>
        /// 门店
        /// </summary>
        [Description("门店")]
        Shop = 1,
        /// <summary>
        /// 平台
        /// </summary>
        [Description("平台")]
        ApolloErp = 2,
    }
}
