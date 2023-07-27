using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.Shop.Service.Core.Enums
{
    /// <summary>
    /// 会员标签
    /// </summary>
    public enum MemberTagEnum
    {
        /// <summary>
        /// 普通会员
        /// </summary>
        [Description("普通会员")] CommonMember = 100,

        /// <summary>
        /// 高级会员
        /// </summary>
        [Description("高级会员")] HighMember = 200,

        /// <summary>
        /// VIP贵宾
        /// </summary>
        [Description("VIP贵宾")] VipMember = 300
    }
}
