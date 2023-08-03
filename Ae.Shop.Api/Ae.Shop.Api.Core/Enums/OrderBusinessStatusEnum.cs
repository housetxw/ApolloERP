using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Enums
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
        /// 待安装
        /// </summary>
        WaitingInstall = 4,
        /// <summary>
        /// 已安装
        /// </summary>
        Installed = 5,
        /// <summary>
        /// 已取消
        /// </summary>
        Canceled = 6,
        /// <summary>
        /// 待对账
        /// </summary>
        WaitingReconciliation = 7,
        /// <summary>
        /// 未知
        /// </summary>
        None = 8,

    }
}
