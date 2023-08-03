using Ae.C.MiniApp.Api.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Response
{
    public class CanReserveDateResponse
    {
        /// <summary>
        /// 门店电话
        /// </summary>
        public string Phone { get; set; } = string.Empty;
        /// <summary>
        /// 预约日期
        /// </summary>
        public List<ReserveDateVO> ReserveDate { get; set; }
        /// <summary>
        /// 预约时间
        /// </summary>
        public List<ReserveTimeVO> ReserveTime { get; set; }
    }
}
