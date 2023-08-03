using Ae.OrderComment.Service.Common.CustomAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.OrderComment.Service.Core.Enums
{
    public enum CheckStatusEnum
    {
        //0待审核 1审核通过 2审核不通过
        [EnumDescription("待审核")]
        待审核 = 0,
        [EnumDescription("审核通过")]
        审核通过=1,
        [EnumDescription("审核不通过")]
        审核不通过
    }
}
