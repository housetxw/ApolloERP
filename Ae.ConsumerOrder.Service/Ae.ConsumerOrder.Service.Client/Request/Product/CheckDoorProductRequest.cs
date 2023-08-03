using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ConsumerOrder.Service.Client.Request
{
    /// <summary>
    /// CheckDoorProductRequest
    /// </summary>
    public class CheckDoorProductRequest
    {
        /// <summary>
        /// 产品pid
        /// </summary>
        public List<string> PidList { get; set; }
    }
}
