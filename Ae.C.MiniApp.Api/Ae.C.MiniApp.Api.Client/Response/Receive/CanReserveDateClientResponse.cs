using Ae.C.MiniApp.Api.Client.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Response
{
    public class CanReserveDateClientResponse
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
