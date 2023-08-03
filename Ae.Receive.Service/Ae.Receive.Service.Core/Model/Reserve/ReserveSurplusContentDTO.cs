﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Model.Reserve
{
    public class ReserveSurplusContentDTO
    {
        /// <summary>
        /// 日期
        /// </summary>
        public string Date { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; } = string.Empty;
    }
}
