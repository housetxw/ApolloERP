using Ae.FMS.Service.Common.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.FMS.Service.Core.Enums
{
    /// <summary>
    /// 付款方式
    /// </summary>
    public enum PayMethodEnum
    {
        /// <summary>
        /// 未设置
        /// </summary>
        [EnumDescription("未设置")]
        Null = 0,
        /// <summary>
        /// 1在线支付
        /// </summary>
        [EnumDescription("在线支付")]
        OnLine = 1,
        /// <summary>
        /// 2到店付款
        /// </summary>
        [EnumDescription("到店付款")]
        ToShop = 2,
        /// <summary>
        ///月结
        /// </summary>
        [EnumDescription("月结")]
        Month = 3
    }
}
