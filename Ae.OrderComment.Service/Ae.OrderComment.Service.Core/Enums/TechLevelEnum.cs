using Ae.OrderComment.Service.Common.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.OrderComment.Service.Core.Enums
{
    public enum TechLevelEnum
    {
        //0待审核 1审核通过 2审核不通过
        [EnumDescription("初级")]
        初级 = 0,
        [EnumDescription("中级")]
        中级,
        [EnumDescription("高级")]
        高级,
        [EnumDescription("资深")]
        资深
    }
}
