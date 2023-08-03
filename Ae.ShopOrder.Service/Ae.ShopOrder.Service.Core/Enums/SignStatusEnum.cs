using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Enums
{ /// <summary>
  /// 签收记录状态枚举
  /// </summary>
    public enum SignRecordStatusEnum
    {
        /// <summary>
        /// 未设置
        /// </summary>
        [Description("未设置")]
        Null = 0,
        /// <summary>
        /// 未签收
        /// </summary>
        [Description("未签收")]
        NotSign = 1,

        /// <summary>
        /// 部分签收
        /// </summary>
        [Description("部分签收")]
        PartSign = 2,

        /// <summary>
        /// 全部签收
        /// </summary>
        [Description("全部签收")]
        HaveSign = 3,

    }
}
