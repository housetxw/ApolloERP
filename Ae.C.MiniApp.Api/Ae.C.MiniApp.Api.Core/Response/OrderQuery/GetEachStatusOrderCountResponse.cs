using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Response
{
    public class GetEachStatusOrderCountResponse
    {
        /// <summary>
        /// 订单列表状态（0全部 1待付款 2待收货 3待安装 4待评价 5售后/退货）
        /// </summary>
        public sbyte OrderListStatus { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }
    }
}
