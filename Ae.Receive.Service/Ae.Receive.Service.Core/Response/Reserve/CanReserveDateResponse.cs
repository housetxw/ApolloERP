using Ae.Receive.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Response
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
        public List<ReserveDateDTO> ReserveDate { get; set; }
        /// <summary>
        /// 预约时间
        /// </summary>
        public List<ReserveTimeDTO> ReserveTime { get; set; }
    }
}
