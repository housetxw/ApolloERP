﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request
{
    public class CancelReserveRequest
    {
        /// <summary>
        /// 预约ID
        /// </summary>
        [Range(1, long.MaxValue, ErrorMessage = "预约ID必须大于0")]

        public long ReserveId { get; set; }
        /// <summary>
        /// 取消原因
        /// </summary>
        public string CancelReason { get; set; }
    }
}
