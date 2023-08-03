﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Ae.C.MiniApp.Api.Core.Request
{
    public class CanReserveOrderListRequest : BasePageRequest
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { get; set; }
    }
}
