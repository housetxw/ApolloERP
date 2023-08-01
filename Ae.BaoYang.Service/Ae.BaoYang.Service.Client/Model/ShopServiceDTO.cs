﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Client.Model
{
    public class ShopServiceDTO
    {
        /// <summary>
        /// 服务Pid
        /// </summary>
        public string Pid { get; set; }

        /// <summary>
        /// 是否上下架
        /// </summary>
        public bool IsOnline { get; set; }
    }
}
