﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Response
{
    public class GetRegionChinaClientResponse
    {
        /// <summary>
        /// 省
        /// </summary>
        public Dictionary<string, string> Provinces { get; set; }
        /// <summary>
        /// 市
        /// </summary>
        public Dictionary<string, string> Citys { get; set; }
        /// <summary>
        /// 区
        /// </summary>
        public Dictionary<string, string> Districts { get; set; }
    }
}
