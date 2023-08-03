using Ae.OrderComment.Service.Common.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.OrderComment.Service.Core.Enums
{
    public enum CommentRewardTypeEnum
    {
        //0未设置 1返现 2积分 3鸡蛋 4鹅蛋

        [EnumDescription("未设置")]
        未设置 = 0,
        [EnumDescription("返现")]
        返现 = 1,
        [EnumDescription("积分")]
        积分 = 2,
        [EnumDescription("鸡蛋")]
        鸡蛋 = 3,
        [EnumDescription("鹅蛋")]
        鹅蛋 = 4,
    }
}
