using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.ConsumerOrder.Service.Core.Enums
{
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
