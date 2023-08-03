using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request.OrderQuery
{
    public class GetOrderServiceTypeRequest : BaseGetRequest
    {
        /// <summary>
        /// 产生类型(0普通产生 1购买核销码产生 2使用核销码产生 3再次购买产生 4追加下单产生 5 上门服务)
        /// </summary>
        public int ProductType { get; set; }

        /// <summary>
        /// 产生类型(0未设置 1轮胎 2保养 3美容 4 钣金维修 5 汽车改装 6 其他)
        /// </summary>
        public int OrderType { get; set; }

        /// <summary>
        /// 产品Pid
        /// </summary>
        public List<string> Pids { get; set; }
    }
}
