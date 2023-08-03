using Ae.OrderComment.Service.Common.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.OrderComment.Service.Core.Enums
{
    public enum ReplyPartTypeEnum
    {
        //0未设置 1门店商家 2官方客服

        [EnumDescription("未设置")]
        未设置 = 0,
        [EnumDescription("门店商家")]
        门店商家=1,

        [EnumDescription("官方客服")]
        官方客服
    }
}
