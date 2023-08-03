using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Dal.Model
{
    public class ReserveTimeNumDO
    {
        /// <summary>
        /// 星期
        /// </summary>
        public int WeekDay { get; set; }
        /// <summary>
        /// 预约日期
        /// </summary>
        public string ReserveDate { get; set; }
        /// <summary>
        /// 预约时间
        /// </summary>
        public DateTime ReserveTime { get; set; }
        /// <summary>
        /// 预约数量
        /// </summary>
        public int ReserveCount {get;set;}
    }
}
