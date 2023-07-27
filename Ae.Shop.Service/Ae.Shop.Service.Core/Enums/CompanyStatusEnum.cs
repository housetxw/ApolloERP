using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.Shop.Service.Core.Enums
{
    /// <summary>
    /// 公司状态
    /// </summary>
    public enum CompanyStatusEnum
    {
        /// <summary>
        /// 待提交
        /// </summary>
        [Description("待提交")]
        ToSubmit = 0,
        /// <summary>
        /// 审核中
        /// </summary>
        [Description("审核中")]
        InReview = 1,
        /// <summary>
        /// 正常
        /// </summary>
        [Description("正常")]
        Normal = 2,
        /// <summary>
        /// 已注销
        /// </summary>
        [Description("已注销")]
        Exit = 3,
    }
}
