using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Order.Service.Dal.Model.Order
{
    /// <summary>
    /// 统计的DO
    /// </summary>
    public class GetStatisticsDO
    {
        /// <summary>
        /// 门店ID
        /// </summary>
        public long ShopId { get; set; }
        /// <summary>
        /// 统计的数量
        /// </summary>
        public int StatisticsNum { get; set; }
    }
}
