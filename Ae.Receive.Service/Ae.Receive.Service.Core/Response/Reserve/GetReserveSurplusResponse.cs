using Ae.Receive.Service.Core.Model;
using Ae.Receive.Service.Core.Model.Reserve;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Response
{
    public class GetReserveSurplusResponse
    {
        /// <summary>
        /// 开始日期
        /// </summary>
        public Array SurplusBegin { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        public Array SurplusEnd { get; set; }
        /// <summary>
        /// 置灰日期
        /// </summary>
        public Array Disabled { get; set; }

        /// <summary>
        /// 日历
        /// </summary>
        public List<ReserveSurplusContentDTO> ReserveSurplus { get; set; }
        /// <summary>
        /// 时间节点
        /// </summary>
        public List<ReserveTimeDTO> ReserveTimeList { get; set; }
        /// <summary>
        /// 预约说明
        /// </summary>
        public string ReserveExplain { get; set; }
    }
}
