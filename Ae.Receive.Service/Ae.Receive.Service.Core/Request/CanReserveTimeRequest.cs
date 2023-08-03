using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Receive.Service.Core.Request
{
    public class CanReserveTimeRequest
    {
        /// <summary>
        /// 门店ID
        /// </summary>
        [Required(ErrorMessage = "门店ID不能为空")]
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
