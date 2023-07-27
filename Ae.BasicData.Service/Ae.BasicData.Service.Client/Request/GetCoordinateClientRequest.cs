using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BasicData.Service.Client.Request
{
    /// <summary>
    /// 根据地址获取坐标
    /// </summary>
    public class GetCoordinateClientRequest
    {
        /// <summary>
        /// 地址
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// key
        /// </summary>
        public string key { get; set; }
    }
}
