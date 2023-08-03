﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request
{
    public class CanReserveTimeV3Request : BaseGetRequest
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
        [Range(2020, 2120, ErrorMessage = "年份必须大于等于当前年份")]
        public int Year { get; set; }
        /// <summary>
        /// 月
        /// </summary>
        [Range(1, 12, ErrorMessage = "月份必须大于0")]
        public int Month { get; set; }
        /// <summary>
        /// 日
        /// </summary>
        [Range(1, 31, ErrorMessage = "日期必须大于0")]
        public int Day { get; set; }
    }
}
