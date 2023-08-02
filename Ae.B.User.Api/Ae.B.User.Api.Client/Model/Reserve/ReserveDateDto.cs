using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.User.Api.Client.Model.Reserve
{
    public class ReserveDateDto
    {
        /// <summary>
        /// 日期 02/03
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// 星期
        /// </summary>
        public string Week { get; set; }

        /// <summary>
        /// 是否全约满
        /// </summary>
        public bool FullReserve { get; set; }

        /// <summary>
        /// 预约记录
        /// </summary>
        public List<ReserveTimeRecordDto> Items { get; set; }
    }

    /// <summary>
    /// 预约时间记录
    /// </summary>
    public class ReserveTimeRecordDto
    {
        /// <summary>
        /// 19:00
        /// </summary>
        public string DatePart { get; set; }

        /// <summary>
        /// 2020-02-03 19:00
        /// </summary>
        public string DateTimePart { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        public bool Enable { get; set; }

        /// <summary>
        /// 总可预约数量
        /// </summary>
        public int? EnableNum { get; set; }

        /// <summary>
        /// 已预约数量
        /// </summary>
        public int? ReservedNum { get; set; }

        /// <summary>
        /// 繁忙
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 是否逾期
        /// </summary>
        public bool Overdue { get; set; }
    }
}
