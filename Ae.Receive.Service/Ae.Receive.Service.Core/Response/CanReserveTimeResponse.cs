using Ae.Receive.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Response
{
    public class CanReserveTimeResponse
    {
        /// <summary>
        /// 显示的预约时间
        /// </summary>
        public List<string> ReserveTimeShow { get; set; }
    }
}
