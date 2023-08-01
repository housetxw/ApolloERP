using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.User.Service.Core.Enums
{
    /// <summary>
    /// 用户操作成长值类型
    /// </summary>
    public enum UserGrowthOperateTypeEnum
    {
        /// <summary>
        /// 购买产品
        /// </summary>
        [Description("购买产品")]
        UserPurchase,

        /// <summary>
        /// 星级到期扣除成长值
        /// </summary>
        [Description("星级到期扣除成长值")]
        DueToDeduct
    }
}
