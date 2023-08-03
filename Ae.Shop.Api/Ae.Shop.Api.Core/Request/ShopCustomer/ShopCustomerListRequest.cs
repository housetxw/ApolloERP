﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Request.ShopCustomer
{
    /// <summary>
    /// 客户列表Request
    /// </summary>
    public class ShopCustomerListRequest:BasePageRequest
    {
        /// <summary>
        /// 客户姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 客户手机号
        /// </summary>
        public string UserTel { get; set; }

        /// <summary>
        /// 到店起始时间：1 一个月前 3 三个月前 6 半年前  12 一年前
        /// </summary>
        public int StartInTime { get; set; }
    }
}
