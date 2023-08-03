using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Model.Reserve
{
    public class ReserveSurplusDTO
    {
        /// <summary>
        /// 开始日期
        /// </summary>
        public int[] SurplusBegin { get; set; } = new int[3];
        /// <summary>
        /// 结束日期
        /// </summary>
        public int[] SurplusEnd { get; set; } = new int[3];
        /// <summary>
        /// 置灰日期
        /// </summary>
        public string[] Disabled { get; set; }
        /// <summary>
        /// 预约日历
        /// </summary>
        public List<ReserveSurplusContentDTO> Items { get; set; } = new List<ReserveSurplusContentDTO>();
        
    }
}
