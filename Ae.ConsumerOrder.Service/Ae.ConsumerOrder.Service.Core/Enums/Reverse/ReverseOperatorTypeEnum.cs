using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.ConsumerOrder.Service.Core.Enums
{
    public enum ReverseOperatorTypeEnum
    {
        /// <summary>
        /// 未设置
        /// </summary>
        [Description("未设置")]
        None = 0,
        /// <summary>
        /// 客户
        /// </summary>
        [Description("客户")]
        Customer = 1,
        /// <summary>
        /// 客服
        /// </summary>
        [Description("客服")]
        CustomerService = 2
    }
}
