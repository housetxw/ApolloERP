using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Model.Arrival
{
    public class ProjectItemVo
    {
        /// <summary>
        /// PID
        /// </summary>
        public string Pid { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Num { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 订单类型
        /// </summary>
        public int OrderType { get; set; }

        /// <summary>
        /// 促销的Id
        /// </summary>
        public string ActivityId { get; set; }
    }
}
