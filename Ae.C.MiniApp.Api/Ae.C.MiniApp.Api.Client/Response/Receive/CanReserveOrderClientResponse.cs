using Ae.C.MiniApp.Api.Client.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Response
{
    public class CanReserveOrderClientResponse
    {
        /// <summary>
        /// 可预约订单
        /// </summary>
        public List<CanReserveOrderDTO> OrderList { get; set; }
    }
}
