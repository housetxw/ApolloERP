using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Enums
{
    public enum ProductTypeEnum
    {
        /// <summary>
        /// 0 普通产生
        /// </summary>
        Ordinary = 0,

        /// <summary>
        /// 1购买核销
        /// </summary>
        BuyVerification = 1,

        /// <summary>
        /// 2 使用核销
        /// </summary>
        UseVerification = 2,

        /// <summary>
        /// 3 再次购买产生
        /// </summary>
        AgainBuy = 3,

        /// <summary>
        /// 4 追加下单产生
        /// </summary>
        AppendOrder = 4,

        /// <summary>
        /// 5 上门服务
        /// </summary>
        DoorToDoor = 5,
        /// <summary>
        /// 6 会员卡
        /// </summary>
        VipCard = 6,

    }
}
