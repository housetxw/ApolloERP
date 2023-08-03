using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Response
{
    public class JudgeMyReserveClientResponse
    {
        /// <summary>
        /// 跳转类型 1：有预约记录 2：无订单预约 3：一条订单预约 4：多条订单预约 
        /// </summary>
        public int Type { get; set; }
    }
}
