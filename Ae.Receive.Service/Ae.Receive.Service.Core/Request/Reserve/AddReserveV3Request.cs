using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Receive.Service.Core.Request.Reserve
{
    public class AddReserveV3Request
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [Required(ErrorMessage = "用户ID不能为空")]
        public string UserId { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }
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
        /// <summary>
        /// 预约时间
        /// </summary>
        [Required(ErrorMessage = "预约时间不能为空")]
        public string ReserveTime { get; set; }

        /// <summary>
        /// 补录订单CarId
        /// </summary>
        public string CarId { get; set; }
    }
}
