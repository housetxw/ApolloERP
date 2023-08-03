using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Model.Arrival
{
    /// <summary>
    /// 项目项目Vo
    /// </summary>
    public class ProjectItemVo
    {
        public string OrderType { get; set; }
        /// <summary>
        /// 订单Id
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 项目Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Num { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public string Price { get; set; }
    }
}
