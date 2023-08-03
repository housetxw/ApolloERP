using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request
{
    public class CanReserveTimeRequest : BaseGetRequest
    {
        /// <summary>
        /// 门店ID
        /// </summary>
        [Range(1, long.MaxValue, ErrorMessage = "门店ID必须大于0")]
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
        [Range(1, 7, ErrorMessage = "偏移值必须大于0，小于8")]
        public int OffsetValue { get; set; }
    }
}
