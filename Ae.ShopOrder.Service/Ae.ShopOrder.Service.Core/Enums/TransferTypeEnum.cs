using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Enums
{

    /// <summary>
    /// 移库类型
    /// </summary>
    public enum TransferTypeEnum
    {
        /// <summary>
        /// 0订单
        /// </summary>
        [Description("订单")]
        Order = 0,
    }

    public enum PackageStatusEnum
    {
        新建 = 1,
        已发出,
        已签收,
        已清点
    }

}
