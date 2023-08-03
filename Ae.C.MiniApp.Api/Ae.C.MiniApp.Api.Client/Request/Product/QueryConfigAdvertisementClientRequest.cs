using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Request.Product
{
    public class QueryConfigAdvertisementClientRequest
    {
        /// <summary>
        /// 终端类型：ShopApp门店管家/CbJApplet门店/YcJApplet养车/QpJApplet汽配
        /// </summary>
        public string TerminalType { get; set; }
    }
}
