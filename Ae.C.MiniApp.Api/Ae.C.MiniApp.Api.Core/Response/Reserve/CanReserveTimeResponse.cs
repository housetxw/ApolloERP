using Ae.C.MiniApp.Api.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Response
{
    public class CanReserveTimeResponse
    {
        /// <summary>
        /// 显示的预约时间
        /// </summary>
        public List<string> ReserveTimeShow { get; set; }
    }
}
