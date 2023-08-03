using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Model
{
    public class ReserveDateDTO
    {
        /// <summary>
        /// 年份
        /// </summary>
        public string Year { get; set; }
        /// <summary>
        /// 预约日期
        /// </summary>
        public string ReserveDate { get; set; }

        /// <summary>
        /// 预约星期
        /// </summary>
        public string ReserveWeekDay { get; set; }
        /// <summary>
        /// 是否高亮
        /// </summary>
        public bool IsHighLight { get; set; }
        /// <summary>
        /// 偏移值
        /// </summary>
        public int OffsetValue { get; set; }
        /// <summary>
        /// 显示的预约时间
        /// </summary>
        public string ReserveTime { get; set; }
    }
}
