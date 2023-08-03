using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Request
{
    public class CanReserveTimeClientRequest
    {
        /// <summary>
        /// 门店ID
        /// </summary>
        public long ShopId { get; set; }
        /// <summary>
        /// 预约ID
        /// </summary>
        public long ReserveId { get; set; }
        /// <summary>
        /// 年份
        /// </summary>
        public string Year { get; set; }
        /// <summary>
        /// 预约日期
        /// </summary>
        public string ReserveDate { get; set; }
        /// <summary>
        /// 偏移值
        /// </summary>
        public int OffsetValue { get; set; }
    }
}
