﻿using Ae.C.MiniApp.Api.Client.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Response
{
    public class GetReserveSurplusClientResponse
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
