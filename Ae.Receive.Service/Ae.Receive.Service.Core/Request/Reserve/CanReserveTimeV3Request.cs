using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Request.Reserve
{
    public class CanReserveTimeV3Request
    {
        /// <summary>
        /// 门店ID
        /// </summary>
        public long ShopId { get; set; }
        /// <summary>
        /// 偏移值
        /// </summary>
        public int OffsetValue { get; set; }
        /// <summary>
        /// 年份
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// 月
        /// </summary>
        public int Month { get; set; }
        /// <summary>
        /// 日
        /// </summary>
        public int Day { get; set; }
        /// <summary>
        /// 0 时间节点  1配置查询
        /// </summary>
        public int Type { get; set; }
    }
}
