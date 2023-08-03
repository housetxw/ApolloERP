using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Request.Order
{
    public class GetOrderDispatchRequest
    {
        /// <summary>
        /// 技师Ids
        /// </summary>
        public List<string> TechIds { get; set; }

        /// <summary>
        /// 开始日期
        /// </summary>
        public string StartTime { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public string EndTime { get; set; }

        /// <summary>
        /// 订单集合
        /// </summary>
        public List<string> OrderNos { get; set; }


    }
}
