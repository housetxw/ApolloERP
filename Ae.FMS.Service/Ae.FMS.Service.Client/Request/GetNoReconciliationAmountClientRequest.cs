﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.FMS.Service.Client.Request
{
   public class GetNoReconciliationAmountClientRequest
    {
        /// <summary>
        /// 门店集合
        /// </summary>
        public List<long> ShopIds { get; set; }
    }
}
