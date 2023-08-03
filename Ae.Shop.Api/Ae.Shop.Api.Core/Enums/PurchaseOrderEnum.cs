using Ae.Shop.Api.Common.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Enums
{
    public enum LogTypeEnum
    {
        Purchase = 1
    }

    public enum LogLevelEnum
    {
        //1Info  2Warning 3Error 4Critical
        Info = 1,
        Warning,
        Error,
        Critical
    }
    //1新建 2待发货 3已取消 4已发货 5部分收货 6已收货

    /// <summary>
    /// 采购主单状态
    /// </summary>
    public enum PurchaseStatusEnum
    {
        [EnumDescription("新建")]
        新建 = 1,
        [EnumDescription("待发货")]
        待发货=2,
        [EnumDescription("已取消")]
        已取消=3,
        [EnumDescription("已发货")]
        已发货=4,
        [EnumDescription("部分收货")]
        部分收货=5,
        [EnumDescription("已收货")]
        已收货=6
    }

    /// <summary>
    /// 采购明细状态
    /// </summary>
    public enum PurchaseOrderStatusEnum
    {
        //状态(1新建 2预约中 3已取消 4待审核 5待财务审核 6已驳回 7审核通过 8已发货 9部分收货 10已收货)
        [EnumDescription("新建")]
        新建 = 1,
        [EnumDescription("预约中")]
        预约中 = 2,
        [EnumDescription("已取消")]
        已取消=3,
        [EnumDescription("待审核")]
        待审核=4,
        [EnumDescription("待财务审核")]
        待财务审核=5,
        [EnumDescription("已驳回")]
        已驳回=6,
        [EnumDescription("审核通过")]
        审核通过=7,
        [EnumDescription("已发货")]
        已发货=8,
        [EnumDescription("部分收货")]
        部分收货=9,
        [EnumDescription("已收货")]
        已收货=10,
        [EnumDescription("已退货")]
        已退货 = 11
    }


}
