using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Enums
{
    /// <summary>
    /// 订单业务状态
    /// </summary>
    public enum OrderBusinessStatusEnum
    {
        /// <summary>
        /// 全部
        /// </summary>
        All = 0,
        /// <summary>
        /// 待签收
        /// </summary>
        WaitingSign = 1,
        /// <summary>
        /// 待派工
        /// </summary>
        WaitingDispatch = 2,
        /// <summary>
        /// 施工中
        /// </summary>
        ABuilding = 3,
        /// <summary>
        /// 已安装
        /// </summary>
        Installed = 4,
        /// <summary>
        /// 已取消
        /// </summary>
        Canceled = 5,
        /// <summary>
        /// 待对账
        /// </summary>
        WaitingReconciliation = 6,
        /// <summary>
        /// 未知
        /// </summary>
        None = 7,

    }
}
