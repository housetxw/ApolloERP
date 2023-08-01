using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Client.Request
{
    public class VehicleConfigClientRequest
    {
        /// <summary>
        /// 五级车型Tid
        /// </summary>
        public List<string> TidList { get; set; }
    }
}
