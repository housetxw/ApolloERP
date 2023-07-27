using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.Shop.Service.Core.Enums
{
    /// <summary>
    /// 门店类型
    /// </summary>
    public enum ShopTypeEnum
    {
        /// <summary>
        /// 仓库
        /// </summary>
        [Description("仓库")]
        Warehouse = -1,
        /// <summary>
        /// 合作门店
        /// </summary>
        [Description("合作门店")]
        CooperativeStore = 1,

        /// <summary>
        /// 自营门店
        /// </summary>
        [Description("自营门店")]
        DirectlyOperatedStore = 2,
        /// <summary>
        /// 上门养车
        /// </summary>
        [Description("上门养车门店")]
        UpperDoorMaintain = 4,
        /// <summary>
        /// 认证店
        /// </summary>
        [Description("认证店")]
        CertificationStore = 8,

        /// <summary>
        /// 平台先生（工作室)
        /// </summary>
        [Description("平台先生")]
        ApolloErpMen=16,

        /// <summary>
        /// 门店前置仓
        /// </summary>
        [Description("门店前置仓")]
        FrontWarehouse = 32
    }

    /// <summary>
    /// 门店上下架状态
    /// </summary>
    public enum ShopOnlineEnum 
    {
        /// <summary>
        /// 下架
        /// </summary>
        [Description("下架")]
        Offline = 0,
        /// <summary>
        /// 上架
        /// </summary>
        [Description("上架")]
        Online = 1,
    }
}
