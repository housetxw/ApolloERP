using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BasicData.Service.Client.Request
{
    public class GetPositionClientRequest
    {
        /// <summary>
        /// 经纬度
        /// </summary>
        public string location { get; set; }
        /// <summary>
        /// key
        /// </summary>
        public string key { get; set; }
    }
}
