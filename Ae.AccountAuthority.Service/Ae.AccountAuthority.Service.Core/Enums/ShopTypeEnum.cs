using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.AccountAuthority.Service.Core.Enums
{
    public enum ShopTypeEnum
    {
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
        /// 红鹅先生（工作室)
        /// </summary>
        [Description("红鹅先生")]
        RedGooseMen=16,

    }
}
