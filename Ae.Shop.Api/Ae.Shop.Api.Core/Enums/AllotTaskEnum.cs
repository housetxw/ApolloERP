using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Enums
{
    public enum AllotTaskStatusEnum
    {
        //部分的场景默认不采用
        //目前使用的状态(待审核 审核未通过 待出库 全部出库 已取消)

        待审核 = 1,
        审核未通过,
        待出库,
        部分出库,
        全部出库,
        部分发货,
        全部发货,
        部分签收,
        全部签收,
        已取消
    }

    public enum ShopOccupyStatusEnum
    {
        正常 =1,
        已失效
    }

}
